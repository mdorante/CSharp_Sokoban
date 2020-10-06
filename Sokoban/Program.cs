using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;

namespace ConsoleApp5
{
    enum Tile
    {
        Piso,
        Pared,
        Boton,
        InicioCaja,
        InicioJugador
    }

    enum Direccion
    {
        Ninguna,
        Norte,
        Sur,
        Oeste,
        Este

    }

    class SOKOBAN
    {
        //***********************************************************************************************************************************************************//
        //   _____       _         _                 
        //  / ____|     | |       | |                
        // | (___   ___ | | _____ | |__   __ _ _ __  
        //  \___ \ / _ \| |/ / _ \| '_ \ / _` | '_ \ 
        //  ____) | (_) |   < (_) | |_) | (_| | | | |
        // |_____/ \___/|_|\_\___/|_.__/ \__,_|_| |_|
        //
        // Programado por:
        //
        // Consignas:

        // - Toda ambigüedad en el enunciado de las funciones debera ser consultada.
        // - Si el programa no compila, no se evalua.
        // - Cada funcion puede estar parcialmente correcta, valiendo un porcentaje del total de su puntaje.
        // - El puntaje de cada funcion es proporcional a su complejidad.
        // - No se debe modificar el Namespace, ni el nombre de la class Program, ni el contenido de los enum
        // - El unico using presente debe ser System.
        // - No utilizar clases ni structs
        // - No modificar la funcion Main
        // - Enviar este archivo "Program.cs" con la resolucion a gabriel.wille@istea.com.ar con asunto: "Primer Parcial ESDA <DIA>" donde <DIA> es el dia de cursada.
        //***********************************************************************************************************************************************************//


        /**************************************************************
          Modelo
          **************************************************************/

        /// <funcion>
        ///  CrearMapa
        /// </funcion>
        /// 
        /// <summary>
        /// Crea una representacion logica de un mapa de Sokoban en base a una string con formato valido.
        /// </summary>
        /// 
        /// <param name="infoMapa">
        /// Una string[] de al menos 4 elementos que contendra caracteres que representaran de manera visual los diferentes tipos de "Tiles" que pueden formar un mapa.
        /// Estos pueden ser:
        /// - ' ' para Piso
        /// - '+' para Pared
        /// - 'B' para Boton
        /// - '#' para InicioCaja 
        /// - 'I' para InicioJugador.
        /// Cada elemento de infoMapa representara una fila de la matriz retorno y cada fila tendra tantas columnas como caracteres haya en el elemento correspondiente.
        /// 
        /// </param>
        /// 
        /// 
        /// <returns> 
        /// Una matriz de Tile con la representacion logica de lo que se encuentra en infoMapa
        /// que tendra tantas filas como elementos tenga infoMapa y tantas columnas como el largo del primer elemento de infoMapa 
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">infoMapa es null</exception>
        /// <exception cref="ArgumentException">infoMapa tiene menos de 4 elementos</exception>
        /// <exception cref="FormatException"> 
        /// infoMapa no tiene formato correcto, esto puede suceder porque:
        ///     - Alguno de los elementos de infoMapa es de diferente largo que el resto.
        ///     - Se encontro un caracter que no corresponde con los enumerados como validos.
        ///     - Hay mas 'B' que '#' o viceversa 
        ///     - No hay 'B' o no hay '#'
        ///     - No hay 'I' o hay mas de uno
        /// </exception>
        /// 
        ///COMPLETE EL CODIGO donde encuentre ?????????????

        public static Tile[,] CrearMapa(string[] infoMapa)
        {
            Tile[,] rv;
            int cantI = 0;
            int cantBotones = 0;
            int cantCajas = 0;

            if (infoMapa == null)
            {
                throw new ArgumentNullException("El parametro infoMapa es null.");
            }

            if (infoMapa.Length < 4)
            {
                throw new ArgumentException("El parametro infoMapa tiene menos de 4 elementos.");
            }


            //Si el elemento 0 es null, se lanza NullReferenceException
            if (infoMapa[0] == null)
            {
                throw new NullReferenceException("El primer elemento de infoMapa es null.");
            }
            else
            {
                rv = new Tile[infoMapa.Length, infoMapa[0].Length];
            }

            //Por cada fila
            for (int f = 0; f < rv.GetLength(0); f++)
            {

                //El elemento actual de info mapa tiene length diferente que la cantidad de columnas de rv.
                if (infoMapa[f].Length != rv.GetLength(1))
                {
                    throw new FormatException("Alguno de los elementos de infoMapa es de diferente largo que el resto.");
                }

                //Por cada Columna
                for (int c = 0; c < rv.GetLength(1); c++)
                {
                    switch (infoMapa[f][c])
                    {
                        case ' ':
                            rv[f, c] = Tile.Piso;
                            break;
                        case '+':
                            rv[f, c] = Tile.Pared;
                            break;
                        case 'B':
                            rv[f, c] = Tile.Boton;
                            cantBotones++;
                            break;
                        case '#':
                            rv[f, c] = Tile.InicioCaja;
                            cantCajas++;
                            break;
                        case 'I':
                            rv[f, c] = Tile.InicioJugador;
                            cantI++;
                            break;
                        default:
                            throw new FormatException("Se encontro un caracter que no corresponde con los enumerados como validos.");
                    }

                }

            }

            if (cantBotones != cantCajas)
            {
                throw new FormatException("Hay mas 'B' que '#' o viceversa ");
            }

            if (cantBotones == 0 || cantCajas == 0)
            {
                throw new FormatException("No hay 'B' o no hay '#'");
            }

            if (cantI == 0 || cantI > 1)
            {
                throw new FormatException("No hay 'I' o hay mas de uno");
            }

            return rv;
        }


        /// <funcion>
        /// CrearCajas
        /// </funcion>
        /// 
        /// <summary>
        /// Crea el mapa de cajas partiendo de una matriz de Tile. 
        /// </summary>
        /// 
        /// <param name="mapa">Una matriz de Tile</param>
        /// 
        /// <returns>Una matriz de bool, con el mismo Length que mapa en ambas dimensiones, donde sus elementos valdran true si en la misma posicion de mapa hay un elemento Tile.InicioCaja y false todos los demas</returns>
        ///
        /// <exception cref="ArgumentNullException">mapa es null</exception>
        /// <exception cref="ArgumentException">Si no hay Tile.InicioCaja en mapa</exception>
        ///
        ///
        ///COMPLETE EL CODIGO donde encuentre ?????????????
        public static bool[,] CrearCajas(Tile[,] mapa)
        {
            bool[,] rv;
            int cantCajas = 0;

            if (mapa == null)
            {
                throw new ArgumentNullException("El argumento mapa es null.");
            }

            rv = new bool[mapa.GetLength(0), mapa.GetLength(1)];

            for (int f = 0; f < mapa.GetLength(0); f++)
            {
                for (int c = 0; c < mapa.GetLength(1); c++)
                {
                    if (mapa[f, c] == Tile.InicioCaja)
                    {
                        rv[f, c] = true;
                        cantCajas++;
                    }
                }
            }

            if (cantCajas == 0)
            {
                throw new ArgumentException("No hay Tile.InicioCaja en mapa");
            }

            return rv;
        }


        /// <funcion>
        /// InicializarJugador
        /// </funcion>
        /// 
        /// <summary>
        /// Setea filaJugador y columnaJugador en la fila y columna correspondiente a la posicion donde encuentre el primer Tile.InicioJugador en mapa.
        /// </summary>
        /// 
        /// <param name="mapa">Una matriz de Tile</param>
        /// <param name="filaJugador">Referencia a un int que almacenara la fila del jugador</param>
        /// <param name="columnaJugador">Referencia a un int que almacenara la columna del jugador</param>
        /// 
        /// <returns> void </returns>
        /// 
        /// <exception cref="ArgumentNullException">mapa es null</exception>
        /// <exception cref="ArgumentException"> Si no hay Tile.InicioJugador presente en mapa </exception>
        /// 
        ///COMPLETE EL CODIGO donde encuentre ?????????????
        public static void InicializarJugador(Tile[,] mapa, ref int filaJugador, ref int columnaJugador)
        {
            if (mapa == null)
            {
                throw new ArgumentNullException("El argumento mapa no puede ser null");
            }


            for (int f = 0; f < mapa.GetLength(0); f++)
            {
                for (int c = 0; c < mapa.GetLength(1); c++)
                {
                    if (mapa[f, c] == Tile.InicioJugador)
                    {
                        filaJugador = f;
                        columnaJugador = c;
                        return;
                    }
                }
            }

            // Si llegamos a esta linea, es porque no se consigue ningun Tile.InicioJugador en el mapa
            throw new ArgumentException("No hay Tile.InicioJugador presente en mapa");

        }


        //Revise la especificacion de la funcion siguiente y arregle esta.
        static void ActualizarMovimiento(Direccion dir, ref int filaJugador, ref int columnaJugador)
        {
            switch (dir)
            {
                case Direccion.Norte:
                    filaJugador--;
                    break;
                case Direccion.Sur:
                    filaJugador++;
                    break;
                case Direccion.Este:
                    columnaJugador++;
                    break;
                case Direccion.Oeste:
                    columnaJugador--;
                    break;
                case Direccion.Ninguna:
                    return;
            }

            return;
        }


        /// <funcion>
        /// EjecutarMovimiento
        /// </funcion>
        /// 
        /// <summary>
        /// Modifica el estado del juego segun el movimiento realizado por el jugador.
        /// Si dir es Ninguna, la funcion termina sin realizar ningun movimiento.
        /// Si dir es Oeste, se reducira columnaJugador en uno siempre y cuando sea un movimiento valido.
        /// Si dir es Este, se incrementara columnaJugador en uno siempre y cuando sea un movimiento valido.
        /// Si dir es Norte, se reducira filaJugador en uno siempre y cuando sea un movimiento valido.
        /// Si dir es Sur, se incrementara filaJugador en uno siempre y cuando sea un movimiento valido.
        /// <IMPORTANTE>
        ///     Un movimiento es valido cuando en la celda destino del jugador hay piso, un boton o una caja y se encuentra dentro de los limites de mapa.
        ///     En el caso particular de que en la celda destino haya una caja, 
        ///     esta no debera tener una pared u otra caja en la celda adyacente en la direccion del movimiento (o que la caja no se encuentre en el limite del mapa).
        ///     Si el movmiento NO es valido, todos los parametros deben permanecer sin cambios.
        /// </IMPORTANTE>
        /// Si el movimiento es valido y en direccion a una caja, esta debe ser desplazada hacia dir, es decir se debe setear en false la celda actual 
        /// y setear en true la celda adyacente en la direccion del movimiento (en la matriz cajas).
        ///  
        /// </summary>
        /// 
        /// <param name="dir">Direccion del movimiento</param>
        /// <param name="mapa">Una matriz de Tile</param>
        /// <param name="cajas">Una matriz de bool que representa la posicion de las cajas</param>
        /// <param name="filaJugador">Referencia a un int que almacenara la fila del jugador</param>
        /// <param name="columnaJugador">Referencia a un int que almacenara la columna del jugador</param>
        /// 
        /// <returns>true si hubo movimiento, false de lo contrario.</returns>
        /// 
        /// <exception cref="ArgumentNullException">Alguna de las matrices es null</exception>
        /// <exception cref="ArgumentException">El tamaño de las dimensiones de mapa y cajas son diferente</exception>
        /// <exception cref="ArgumentOutOfRangeException">filaJugador o columnaJugador se encuentra fuera de los limites de mapa</exception>
        /// 
        ///COMPLETE EL CODIGO donde encuentre ?????????????
        public static bool EjecutarMovimiento(Direccion dir, Tile[,] mapa, bool[,] cajas, ref int filaJugador, ref int columnaJugador)
        {

            int nuevaFilaJugador = filaJugador;
            int nuevaColumnaJugador = columnaJugador;

            if (mapa == null || cajas == null)
            {
                throw new ArgumentNullException("Alguna de las matrices es null");
            }


            if (mapa.Length != cajas.Length || mapa.GetLength(0) != cajas.GetLength(0) || mapa.GetLength(1) != cajas.GetLength(1))
            {
                throw new ArgumentException("El tamaño de las dimensiones de mapa y cajas son diferente");
            }

            if (dir == Direccion.Ninguna)
            {
                return false;
            }


            ActualizarMovimiento(dir, ref nuevaFilaJugador, ref nuevaColumnaJugador);


            if (nuevaFilaJugador < 0 || nuevaFilaJugador >= mapa.GetLength(0) ||
                 nuevaColumnaJugador < 0 || nuevaColumnaJugador >= mapa.GetLength(1))
            {
                return false;
            }


            if (mapa[nuevaFilaJugador, nuevaColumnaJugador] == Tile.Pared)
            {
                return false;
            }

            //Hay una caja en la celda destino!
            if (cajas[nuevaFilaJugador, nuevaColumnaJugador])
            {
                int facaja = nuevaFilaJugador + (nuevaFilaJugador - filaJugador);
                int cacaja = nuevaColumnaJugador + (nuevaColumnaJugador - columnaJugador);

                if (facaja < 0 || facaja >= mapa.GetLength(0) || cacaja < 0 || cacaja >= mapa.GetLength(1))
                    return false;


                if (!cajas[facaja, cacaja] && mapa[facaja, cacaja] != Tile.Pared)
                {
                    cajas[facaja, cacaja] = true;
                    cajas[nuevaFilaJugador, nuevaColumnaJugador] = false;
                }
                else
                {
                    return false;
                }

            }

            filaJugador = nuevaFilaJugador;
            columnaJugador = nuevaColumnaJugador;

            return true;
        }


        /// <summary>
        /// Indica si el mapa fue resuelto exitosamente
        /// Sera victoria cuando los elementos true de cajas se encuentren en las mismas posiciones que los elementos Tile.Boton de mapa.
        /// </summary>
        /// 
        /// <param name="mapa">Una matriz de Tile</param>
        /// <param name="cajas">Una matriz de bool que representa la posicion de las cajas</param>
        /// 
        /// <returns>true si todas las cajas estan sobre un boton, false de lo contrario</returns>
        /// 
        /// <exception cref="ArgumentNullException">Alguna de las matrices es null</exception>
        /// 
        ///COMPLETE EL CODIGO donde encuentre ?????????????
        public static bool EsVictoria(Tile[,] mapa, bool[,] cajas)
        {
            if (mapa == null || cajas == null)
            {
                throw new ArgumentNullException("Alguna de las matrices es null");
            }

            for (int f = 0; f < mapa.GetLength(0); f++)
            {
                for (int c = 0; c < mapa.GetLength(1); c++)
                {
                    if (cajas[f, c] && mapa[f, c] != Tile.Boton)
                    {
                        return false;
                    }
                }
            }

            // Si llegamos hasta aqui, es porque todos los elementos true de cajas coinciden con un boton de mapa
            return true;
        }



        /**************************************************************
         Vista y Control
         **************************************************************/




        /// <summary>
        /// Retrona una direccion de movimiento segun input del usuario.
        /// Las teclas validas son 
        /// - ConsoleKey.UpArrow  -> Norte
        /// - ConsoleKey.DownArrow -> Sur
        /// - ConsoleKey.LeftArrow -> Oeste
        /// - ConsoleKey.RightArrow -> Este
        /// </summary>
        /// <returns> Retrona una direccion de movimiento segun input del usuario. Si el usuario presiona una tecla no contemplada, retorna Direccion.Ninguna </returns>
        /// 
        ///COMPLETE EL CODIGO donde encuentre ?????????????
        public static Direccion ObtenerDireccionMovimiento()
        {
            Direccion rv = Direccion.Ninguna;


            //Esperamos a que el usuario presione una tecla.
            ConsoleKeyInfo cki = Console.ReadKey(true);

            //Utilizar la variable tecla para definir el resultado de esta funcion.
            ConsoleKey tecla = cki.Key;

            switch (tecla)
            {
                case ConsoleKey.UpArrow:
                    rv = Direccion.Norte;
                    break;
                case ConsoleKey.DownArrow:
                    rv = Direccion.Sur;
                    break;
                case ConsoleKey.LeftArrow:
                    rv = Direccion.Oeste;
                    break;
                case ConsoleKey.RightArrow:
                    rv = Direccion.Este;
                    break;
                default:
                    return Direccion.Ninguna;
            }


            return rv;
        }

        /// <summary>
        /// Dibuja un char del color deseado.
        /// </summary>
        /// <param name="unChar"></param>
        /// <param name="color"></param>
        /// 
        public static void Dibujar(char unChar, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(unChar);
            Console.ResetColor();
        }

        /// <summary>
        /// Dibuja un char con la configuracion por defecto de la consola.
        /// </summary>
        /// <param name="unChar"></param>
        /// 
        public static void Dibujar(char unChar)
        {
            Console.ResetColor();
            Console.Write(unChar);

        }

        /// <summary>
        /// Limpia la consola
        /// </summary>
        /// 
        public static void Limpiar()
        {
            Console.Clear();
        }


        /// <funcion>
        /// DibujarJuego
        /// </funcion>
        /// 
        /// <summary>
        /// Dibuja en consola el estado actual del juego. (Es importante respetar el orden de dibujado segun lo propuesto a continuacion)
        /// - El jugador debe dibujarse como '&' de color ConsoleColor.Yellow
        /// - Las paredes como '+' de color ConsoleColor.DarkGray.
        /// - Las cajas deben ser dibujadas como '#' ConsoleColor.White siempre que no se encuentren ubicadas sobre un boton, si estan sobre un boton, seran ConsoleColor.Green.
        /// - Los botones como '*' de color ConsoleColor.Red, siempre y cuando no haya una caja sobre el, si hay una caja o un jugador en la misma posicion, el boton no se dibuja.
        /// - Todo lo demas como un ' '
        /// </summary>
        /// 
        /// <param name="mapa">Una matriz de Tile representando el mapa actual</param>
        /// <param name="cajas">Una matriz de bool representando el mapa de cajas. En la posicion donde haya una caja debera haber un true.</param>
        /// <param name="filaJugador">Fila donde se encuentra el jugador</param>
        /// <param name="columnaJugador">Columna donde se encuentra el jugador</param>
        /// 
        /// <returns> void </returns>
        /// 
        /// <exception cref="ArgumentNullException">Alguna de las matrices es null</exception>
        /// <exception cref="ArgumentException">El tamaño de las dimensiones de mapa y cajas son diferente</exception>
        /// 
        ///COMPLETE EL CODIGO donde encuentre ?????????????
        public static void DibujarJuego(Tile[,] mapa, bool[,] cajas, int filaJugador, int columnaJugador)
        {
            for (int f = 0; f < mapa.GetLength(0); f++)
            {
                for (int c = 0; c < mapa.GetLength(1); c++)
                {
                    switch (mapa[f, c])
                    {
                        case Tile.InicioJugador:
                            if (f == filaJugador && c == columnaJugador)
                            {
                                Dibujar('&', ConsoleColor.Yellow);
                            }
                            else
                            {
                                Dibujar(' ');
                            }
                            break;
                        case Tile.Pared:
                            Dibujar('+', ConsoleColor.DarkGray);
                            break;
                        case Tile.InicioCaja:
                            if (f == filaJugador && c == columnaJugador)
                            {
                                Dibujar('&', ConsoleColor.Yellow);
                            }
                            else if (cajas[f, c])
                            {
                                Dibujar('#', ConsoleColor.White);
                            }
                            else
                            {
                                Dibujar(' ');
                            }
                            break;
                        case Tile.Boton:
                            if (cajas[f, c])
                            {
                                Dibujar('#', ConsoleColor.Green);
                            }
                            else if (f == filaJugador && c == columnaJugador)
                            {
                                Dibujar('&', ConsoleColor.Yellow);
                            }
                            else
                            {
                                Dibujar('*', ConsoleColor.Red);
                            }
                            break;
                        case Tile.Piso:
                            if (f == filaJugador && c == columnaJugador)
                            {
                                Dibujar('&', ConsoleColor.Yellow);
                            }
                            else if (cajas[f, c])
                            {
                                Dibujar('#', ConsoleColor.White);
                            }
                            else
                            {
                                Dibujar(' ');
                            }
                            break;
                    }
                }
                Console.WriteLine();
            }


        }

        /// <funcion>
        /// DibujarMensajesDelSistema
        /// </funcion>
        /// 
        /// <summary>
        /// Escribe en la consola msjs siempre y cuando no sea null. 
        /// </summary>
        /// 
        /// <param name="msjs">Una string con el mensaje a mostrar</param>
        /// 
        /// <returns> void </returns>
        /// 
        public static void DibujarMensajesDelSistema(string msjs)
        {
            Console.WriteLine(msjs);
        }



        /// <summary>
        /// Funcion principal de renderizado.
        /// </summary>
        /// 
        /// <param name="mapa">Una matriz de Tile representando el mapa actual</param>
        /// <param name="cajas">Una matriz de bool representando el mapa de cajas. En la posicion donde haya una caja debera haber un true.</param>
        /// <param name="filaJugador">Fila donde se encuentra el jugador</param>
        /// <param name="columnaJugador">Columna donde se encuentra el jugador</param>
        /// <param name="msjs">Una string con el mensaje a mostrar</param>
        /// 
        /// <returns> void </returns>
        /// 
        public static void Renderizar(Tile[,] mapa, bool[,] cajas, int filaJugador, int columnaJugador, string msjs)
        {
            Limpiar();
            DibujarJuego(mapa, cajas, filaJugador, columnaJugador);
            DibujarMensajesDelSistema(msjs);
        }


        /// <summary>
        /// Logica del juego
        /// </summary>
        /// <param name="infoMapa">      
        /// Una string[] de al menos 4 elementos que contendra caracteres que representaran de manera visual los diferentes tipos de "Tiles" que pueden formar un mapa.
        /// Estos pueden ser:
        /// - ' ' para Piso
        /// - '+' para Pared
        /// - 'B' para Boton
        /// - '#' para InicioCaja 
        /// - 'I' para InicioJugador.
        /// Cada elemento de infoMapa representara una fila de la matriz retorno y cada fila tendra tantas columnas como caracteres haya en el elemento correspondiente.
        /// </param>
        /// 
        /// <returns> void </returns>
        /// 
        public static void Jugar(string[] infoMapa)
        {
            Tile[,] mapa;
            bool[,] cajas;
            int filaJugador = 0;
            int columnaJugador = 0;
            string msjs = null;
            int movimientos = 0;


            //Inicializar las variables.
            mapa = CrearMapa(infoMapa);
            cajas = CrearCajas(mapa);
            InicializarJugador(mapa, ref filaJugador, ref columnaJugador);

            //Renderizar el juego
            Renderizar(mapa, cajas, filaJugador, columnaJugador, msjs);


            while (!EsVictoria(mapa, cajas))
            {

                //Obtener input del usuario.
                Direccion dir = ObtenerDireccionMovimiento();

                if (EjecutarMovimiento(dir, mapa, cajas, ref filaJugador, ref columnaJugador))
                {
                    movimientos++;
                    msjs = $"{movimientos} movimientos";
                }

                //Renderizar el juego
                Renderizar(mapa, cajas, filaJugador, columnaJugador, msjs);


            }

            msjs = $"Victoria en {movimientos} movimientos!!";
            //Renderizar el juego
            Renderizar(mapa, cajas, filaJugador, columnaJugador, msjs);

        }





        static void Main(string[] args)
        {

            string[] lv = new string[] { "+++++++++++",
                                         "+    # #BB+",
                                         "+    # #BB+",
                                         "+I        +",
                                         "+++++++++++"};

            Jugar(lv);

            Console.ReadLine();
        }
    }
}