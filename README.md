# Tarea 9 - Conexión a GastosDB

Programa de consola en C# que se conecta a SQL Server LocalDB con ADO.NET, lee los gastos con un SELECT, los muestra y calcula el total.

## Cómo ejecutarlo
1. Ejecutar el script GastosDB.sql contra (localdb)\MSSQLLocalDB.
2. Abrir el proyecto en Visual Studio Community 2026.
3. Presionar F5 o Ctrl+F5.

## Escenario 1: con los 3 gastos


![Escenario 1](capturas/esc1.png)



## Escenario 2: cuarto gasto (Café, Q12.00, Comida)


![Escenario 2](capturas/esc2.png)



## Escenario 3: tabla vacía


![Escenario 3](capturas/esc3.png)



## Escenario 4: base de datos inexistente


![Escenario 4](capturas/esc4.png)
 Por qué ocurre: cambié el nombre de la base en la cadena de conexión por uno que no existe (GastosDBx). Cuando el programa ejecuta conexion.Open(), SQL Server no encuentra esa base y rechaza la conexión. Esto genera una SqlException con el error 4060. Como todavía no usamos try/catch para manejar la excepción, el programa se cierra de golpe.