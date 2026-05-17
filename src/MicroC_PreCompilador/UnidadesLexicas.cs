using System.Collections.Generic;

namespace MicroC_PreCompilador
{
    public class UnidadesLexicas
    {
        public Dictionary<string, int> PalabrasReservadas =
            new Dictionary<string, int>()
        {
            {"int", 100},
            {"float", 101},
            {"if", 102},
            {"else", 103},
            {"while", 104},
            {"for", 105},
            {"return", 106},
            {"main", 107},
            {"void", 108}
        };

        public Dictionary<string, int> Operadores =
            new Dictionary<string, int>()
        {
            {"+", 200},
            {"-", 201},
            {"*", 202},
            {"/", 203},
            {"=", 204},
            {"==", 205}
        };

        public Dictionary<string, int> Delimitadores =
            new Dictionary<string, int>()
        {
            {";", 300},
            {"(", 301},
            {")", 302},
            {"{", 303},
            {"}", 304}
        };
    }
}