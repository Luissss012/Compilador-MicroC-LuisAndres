using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroC_PreCompilador
{
    public class AnalizadorLexico
    {
        UnidadesLexicas unidades = new UnidadesLexicas();

        public List<Token> Analizar(string codigo)
        {
            List<Token> resultado = new List<Token>();

            char[] caracteres = codigo.ToCharArray();

            string actual = "";
            bool enString = false;

            int linea = 1;

            for (int i = 0; i < caracteres.Length; i++)
            {
                char c = caracteres[i];

                // Contar líneas
                if (c == '\n')
                {
                    linea++;
                }

                // Manejo de strings
                if (c == '"')
                {
                    if (enString)
                    {
                        actual += c;

                        resultado.Add(new Token
                        {
                            Linea = linea,
                            Codigo = 700,
                            Lexema = actual,
                            Tipo = "STRING"
                        });

                        actual = "";
                        enString = false;
                    }
                    else
                    {
                        if (actual != "")
                        {
                            ClasificarToken(actual, resultado, linea);
                            actual = "";
                        }

                        enString = true;
                        actual += c;
                    }

                    continue;
                }

                // Seguir construyendo string
                if (enString)
                {
                    actual += c;
                    continue;
                }

                // Letras y números
                if (char.IsLetterOrDigit(c))
                {
                    actual += c;
                }
                else
                {
                    // Clasificar token acumulado
                    if (actual != "")
                    {
                        ClasificarToken(actual, resultado, linea);
                        actual = "";
                    }

                    // Ignorar espacios
                    if (!char.IsWhiteSpace(c))
                    {
                        string simbolo = c.ToString();

                        // Operadores dobles
                        if (i + 1 < caracteres.Length)
                        {
                            string doble = simbolo + caracteres[i + 1];

                            if (unidades.Operadores.ContainsKey(doble))
                            {
                                resultado.Add(new Token
                                {
                                    Linea = linea,
                                    Codigo = unidades.Operadores[doble],
                                    Lexema = doble,
                                    Tipo = "OPERADOR"
                                });

                                i++;
                                continue;
                            }
                        }

                        // Operadores simples
                        if (unidades.Operadores.ContainsKey(simbolo))
                        {
                            resultado.Add(new Token
                            {
                                Linea = linea,
                                Codigo = unidades.Operadores[simbolo],
                                Lexema = simbolo,
                                Tipo = "OPERADOR"
                            });
                        }
                        // Delimitadores
                        else if (unidades.Delimitadores.ContainsKey(simbolo))
                        {
                            resultado.Add(new Token
                            {
                                Linea = linea,
                                Codigo = unidades.Delimitadores[simbolo],
                                Lexema = simbolo,
                                Tipo = "DELIMITADOR"
                            });
                        }
                        // Error léxico
                        else
                        {
                            resultado.Add(new Token
                            {
                                Linea = linea,
                                Codigo = -1,
                                Lexema = simbolo,
                                Tipo = "ERROR_LEXICO"
                            });
                        }
                    }
                }
            }

            // Último token
            if (actual != "")
            {
                ClasificarToken(actual, resultado, linea);
            }

            // Error de string no cerrado
            if (enString)
            {
                resultado.Add(new Token
                {
                    Linea = linea,
                    Codigo = -1,
                    Lexema = "cadena no cerrada",
                    Tipo = "ERROR_LEXICO"
                });
            }

            return resultado;
        }

        private void ClasificarToken(string token, List<Token> resultado, int linea)
        {
            // Número
            if (token.All(char.IsDigit))
            {
                resultado.Add(new Token
                {
                    Linea = linea,
                    Codigo = 500,
                    Lexema = token,
                    Tipo = "NUMERO"
                });
            }
            // Identificador inválido
            else if (char.IsDigit(token[0]) && token.Any(char.IsLetter))
            {
                resultado.Add(new Token
                {
                    Linea = linea,
                    Codigo = -1,
                    Lexema = token,
                    Tipo = "ERROR_LEXICO"
                });
            }
            // Palabra reservada
            else if (unidades.PalabrasReservadas.ContainsKey(token))
            {
                resultado.Add(new Token
                {
                    Linea = linea,
                    Codigo = unidades.PalabrasReservadas[token],
                    Lexema = token,
                    Tipo = "PALABRA_RESERVADA"
                });
            }
            // Identificador
            else
            {
                resultado.Add(new Token
                {
                    Linea = linea,
                    Codigo = 600,
                    Lexema = token,
                    Tipo = "IDENTIFICADOR"
                });
            }
        }
    }
}