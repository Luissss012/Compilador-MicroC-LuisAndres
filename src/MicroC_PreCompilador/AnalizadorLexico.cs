using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroC_PreCompilador
{
    public class AnalizadorLexico
    {
        // Instancia de la clase que contiene
        // palabras reservadas, operadores y delimitadores
        UnidadesLexicas unidades = new UnidadesLexicas();

        // Método principal del analizador léxico
        public List<Token> Analizar(string codigo)
        {
            // Lista donde se almacenarán los tokens encontrados
            List<Token> resultado = new List<Token>();

            // Convertir el código a arreglo de caracteres
            char[] archivo = codigo.ToCharArray();

            // Contador principal del recorrido
            int cont = 0;

            // Control de líneas
            int linea = 1;

            // Mientras no termine el archivo
            while (cont < archivo.Length)
            {
                // Obtener carácter actual
                char c = archivo[cont];

                // ---------------------------------------------------
                // ¿Es letra o guion bajo?
                // Ejecutar función IdentificadorPalabraReservada
                // ---------------------------------------------------
                if (char.IsLetter(c) || c == '_')
                {
                    IdentificadorPalabraReservada(
                        archivo,
                        ref cont,
                        resultado,
                        linea
                    );
                }

                // ---------------------------------------------------
                // ¿Es número, punto, signo + o signo -?
                // Ejecutar función EnteroReal
                // ---------------------------------------------------
                else if (
                    char.IsDigit(c) ||
                    c == '.' ||
                    c == '+' ||
                    c == '-'
                )
                {
                    EnteroReal(
                        archivo,
                        ref cont,
                        resultado,
                        linea
                    );
                }

                // ---------------------------------------------------
                // ¿Es diagonal?
                // Ejecutar función AutomataComentario
                // ---------------------------------------------------
                else if (c == '/')
                {
                    AutomataComentario(
                        archivo,
                        ref cont,
                        resultado,
                        linea
                    );
                }

                // ---------------------------------------------------
                // ¿Es espacio, tabulador o carácter nulo?
                // Ignorar y avanzar
                // ---------------------------------------------------
                else if (
                    c == ' ' ||
                    c == '\t' ||
                    c == '\0'
                )
                {
                    cont++;
                }

                // ---------------------------------------------------
                // ¿Es salto de línea o retorno de carro?
                // Incrementar línea
                // ---------------------------------------------------
                else if (
                    c == '\n' ||
                    c == '\r'
                )
                {
                    linea++;
                    cont++;
                }

                // ---------------------------------------------------
                // ¿Es delimitador?
                // Agregar token
                // ---------------------------------------------------
                else if (
                    unidades.Delimitadores.ContainsKey(c.ToString())
                )
                {
                    string lexema = c.ToString();

                    resultado.Add(new Token
                    {
                        Linea = linea,
                        Codigo = unidades.Delimitadores[lexema],
                        Lexema = lexema,
                        Tipo = "DELIMITADOR"
                    });

                    cont++;
                }

                // ---------------------------------------------------
                // ¿Es operador?
                // Agregar token
                // ---------------------------------------------------
                else if (
                    unidades.Operadores.ContainsKey(c.ToString())
                )
                {
                    string lexema = c.ToString();

                    resultado.Add(new Token
                    {
                        Linea = linea,
                        Codigo = unidades.Operadores[lexema],
                        Lexema = lexema,
                        Tipo = "OPERADOR"
                    });

                    cont++;
                }

                // ---------------------------------------------------
                // Símbolo no encontrado
                // Error léxico
                // ---------------------------------------------------
                else
                {
                    resultado.Add(new Token
                    {
                        Linea = linea,
                        Codigo = -1,
                        Lexema = c.ToString(),
                        Tipo = "ERROR_LEXICO"
                    });

                    cont++;
                }
            }

            // Retornar lista final de tokens
            return resultado;
        }

        // =========================================================
        // AUTOMATA IDENTIFICADOR / PALABRA RESERVADA
        // =========================================================
        private void IdentificadorPalabraReservada(
            char[] archivo,
            ref int cont,
            List<Token> resultado,
            int linea
        )
        {
            string lexema = "";

            // Construir lexema
            while (
                cont < archivo.Length &&
                (
                    char.IsLetterOrDigit(archivo[cont]) ||
                    archivo[cont] == '_'
                )
            )
            {
                lexema += archivo[cont];
                cont++;
            }

            // ¿Es palabra reservada?
            if (unidades.PalabrasReservadas.ContainsKey(lexema))
            {
                resultado.Add(new Token
                {
                    Linea = linea,
                    Codigo = unidades.PalabrasReservadas[lexema],
                    Lexema = lexema,
                    Tipo = "PALABRA_RESERVADA"
                });
            }

            // Si no, es identificador
            else
            {
                resultado.Add(new Token
                {
                    Linea = linea,
                    Codigo = 600,
                    Lexema = lexema,
                    Tipo = "IDENTIFICADOR"
                });
            }
        }

        // =========================================================
        // AUTOMATA ENTERO / REAL
        // =========================================================
        private void EnteroReal(
            char[] archivo,
            ref int cont,
            List<Token> resultado,
            int linea
        )
        {
            string lexema = "";
            bool puntoDecimal = false;

            // Signo opcional
            if (
                archivo[cont] == '+' ||
                archivo[cont] == '-'
            )
            {
                lexema += archivo[cont];
                cont++;
            }

            // Construcción del número
            while (cont < archivo.Length)
            {
                char c = archivo[cont];

                // Números
                if (char.IsDigit(c))
                {
                    lexema += c;
                }

                // Punto decimal
                else if (c == '.' && !puntoDecimal)
                {
                    lexema += c;
                    puntoDecimal = true;
                }

                else
                {
                    break;
                }

                cont++;
            }

            // Validar si realmente es número
            double numero;

            if (double.TryParse(lexema, out numero))
            {
                resultado.Add(new Token
                {
                    Linea = linea,
                    Codigo = 500,
                    Lexema = lexema,
                    Tipo = "NUMERO"
                });
            }

            // Error léxico
            else
            {
                resultado.Add(new Token
                {
                    Linea = linea,
                    Codigo = -1,
                    Lexema = lexema,
                    Tipo = "ERROR_LEXICO"
                });
            }
        }

        // =========================================================
        // AUTOMATA COMENTARIO
        // =========================================================
        private void AutomataComentario(
            char[] archivo,
            ref int cont,
            List<Token> resultado,
            int linea
        )
        {
            string lexema = "";

            // Verificar si es comentario //
            if (
                cont + 1 < archivo.Length &&
                archivo[cont + 1] == '/'
            )
            {
                lexema += "//";

                cont += 2;

                // Leer comentario completo
                while (
                    cont < archivo.Length &&
                    archivo[cont] != '\n'
                )
                {
                    lexema += archivo[cont];
                    cont++;
                }

                resultado.Add(new Token
                {
                    Linea = linea,
                    Codigo = 800,
                    Lexema = lexema,
                    Tipo = "COMENTARIO"
                });
            }

            // Si no es comentario, es operador división
            else
            {
                resultado.Add(new Token
                {
                    Linea = linea,
                    Codigo = unidades.Operadores["/"],
                    Lexema = "/",
                    Tipo = "OPERADOR"
                });

                cont++;
            }
        }
    }
}