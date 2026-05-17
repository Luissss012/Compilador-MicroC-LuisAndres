# MicroC_PreCompilador

## Descripción del Proyecto

MicroC_PreCompilador es una aplicación desarrollada en C# con Windows Forms que implementa las primeras etapas de un compilador para un lenguaje simplificado inspirado en C.

El proyecto fue creado con el objetivo de comprender el funcionamiento interno de un compilador, especialmente el análisis léxico, la clasificación de tokens y la aplicación de teoría de autómatas.

El sistema permite:

- Leer código fuente carácter por carácter.
- Detectar palabras reservadas.
- Reconocer identificadores.
- Analizar números enteros y reales.
- Detectar operadores y delimitadores.
- Procesar comentarios.
- Detectar errores léxicos.
- Generar una lista de tokens.

Además, el proyecto sigue una arquitectura modular orientada a objetos utilizando clases separadas para el manejo de tokens, tablas léxicas y análisis del código fuente.

---

# Tecnologías Utilizadas

| Tecnología | Uso |
|---|---|
| C# | Lenguaje principal |
| Windows Forms | Interfaz gráfica |
| .NET Framework | Entorno de desarrollo |
| Visual Studio | IDE principal |
| Git & GitHub | Control de versiones |

---

# Arquitectura del Proyecto

El proyecto se encuentra dividido en distintas clases para organizar el funcionamiento del compilador.

## FormPrincipal

Contiene la interfaz gráfica del sistema y permite:

- Crear archivos
- Abrir archivos
- Guardar archivos
- Ejecutar el análisis léxico
- Mostrar resultados del análisis

---

## Token

Representa cada unidad léxica encontrada durante el análisis.

Cada token contiene:

- Línea
- Código
- Lexema
- Tipo

---

## UnidadesLexicas

Contiene las tablas léxicas del lenguaje:

- Palabras reservadas
- Operadores
- Delimitadores

---

## AnalizadorLexico

Es el núcleo principal del proyecto.

Se encarga de:

- Recorrer el código fuente carácter por carácter
- Ejecutar autómatas
- Clasificar tokens
- Detectar errores léxicos
- Generar la lista final de resultados

---

# Funcionamiento del Analizador Léxico

El analizador sigue un flujo basado en autómatas y diagramas de decisión.

## Flujo principal

1. Leer carácter actual.
2. Verificar si es letra o guion bajo.
3. Ejecutar autómata de identificadores y palabras reservadas.
4. Verificar si es número.
5. Ejecutar autómata de números enteros y reales.
6. Verificar si es comentario.
7. Ejecutar autómata de comentarios.
8. Detectar operadores y delimitadores.
9. Detectar errores léxicos.
10. Agregar token a la lista final.

---

# Autómatas Implementados

## Autómata de Identificadores

Permite reconocer:

- Variables
- Nombres válidos
- Palabras reservadas

Ejemplo:

```c
int edad;
float altura;
```

---

## Autómata Entero / Real

Permite detectar:

- Números enteros
- Números decimales
- Signos positivos y negativos

Ejemplo:

```c
10
25.5
-3
+7
```

---

## Autómata de Comentarios

Permite reconocer comentarios de una línea utilizando:

```c
// comentario
```

---

# Detección de Errores Léxicos

El sistema detecta símbolos no válidos dentro del lenguaje.

Ejemplo:

```c
@
#
```

También detecta identificadores inválidos.

Ejemplo:

```c
9edad
```

---

# Ejemplo de Código Analizado

```c
int main()
{
    int edad = 20;

    // comentario

    if (edad >= 18)
    {
        edad = edad + 1;
    }

    return 0;
}
```

---

# Resultado del Análisis

El compilador genera una lista de tokens clasificando:

- Palabras reservadas
- Identificadores
- Operadores
- Delimitadores
- Números
- Comentarios
- Errores léxicos

---

# Capturas del Proyecto

## Interfaz gráfica

<img width="1902" height="1000" alt="image" src="https://github.com/user-attachments/assets/a0371feb-3e95-49de-b07d-8fc09b6f34ff" />


---

## Analizador Léxico funcionando

<img width="838" height="873" alt="image" src="https://github.com/user-attachments/assets/2745efa5-2f4c-46aa-89af-d49f6b0b3d39" />


---

## Ejemplo de Tokens Generados

<img width="647" height="208" alt="image" src="https://github.com/user-attachments/assets/786a185d-124b-41e2-8e3e-fcd5cb617a63" />


---

# Video Explicativo

## Explicación del proyecto y del analizador léxico

https://youtu.be/Bm8CbVcS4tY

---

## Ejemplo práctico de funcionamiento

https://youtu.be/SZCwY-wceus

---

# Commits Realizados

| Commit | Descripción |
|---|---|
| init | Creación inicial del repositorio |
| feat | Interfaz gráfica base |
| feat | Implementación de abrir archivos |
| feat | Implementación de guardar archivos |
| feat | Simulación de compilación |
| feat | Implementación de análisis léxico |
| feat | Detección de palabras reservadas |
| feat | Detección de identificadores |
| feat | Detección de números |
| feat | Implementación de autómata de comentarios |
| feat | Detección de errores léxicos |
| docs | Actualización del README |
| docs | Agregado de capturas |
| docs | Agregado de videos demostrativos |

---

# Conclusión

Este proyecto permitió comprender el funcionamiento interno de un compilador mediante la implementación de análisis léxico y teoría de autómatas.

Además, permitió reforzar conocimientos de:

- Programación orientada a objetos
- Estructuras léxicas
- Diagramas de flujo
- Procesamiento de cadenas
- Diseño modular
- Control de versiones con Git y GitHub

El proyecto representa una simulación funcional de las primeras etapas de un compilador real.

---

# Autor

**Luis Andrés**  
Práctica fase final — Autómatas y Lenguajes
