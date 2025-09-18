using shared;

class StrongestBeam
{
    static void Main()
    {
        string response;

        do
        {
            try
            {
              Console.Write("Ingrese la viga: ");
                string viga = Console.ReadLine()?? "";

                if (string.IsNullOrEmpty(viga))
                {
                    Console.WriteLine("La viga está mal construida!");
                    continue;
                }

            
                int capacidad;
                switch (viga[0])
                {
                    case '%': capacidad = 10; break;
                    case '&': capacidad = 30; break;
                    case '#': capacidad = 90; break;
                    default:
                        Console.WriteLine("La viga está mal construida!");
                        continue;
                }

                // 2. Validar caracteres permitidos
                for (int i = 1; i < viga.Length; i++)
                {
                    if (viga[i] != '=' && viga[i] != '*')
                    {
                        Console.WriteLine("La viga está mal construida!");
                        goto Preguntar; // salta a la pregunta de continuar
                    }
                }

                // 3. Reglas de construcción: no puede iniciar con '*' después de la base, ni tener "**"
                if (viga.Length > 1 && viga[1] == '*')
                {
                    Console.WriteLine("La viga está mal construida!");
                    goto Preguntar;
                }
                if (viga.Contains("**"))
                {
                    Console.WriteLine("La viga está mal construida!");
                    goto Preguntar;
                }

                // 4. Calcular peso
                long pesoTotal = 0;
                int iPos = 1;

                while (iPos < viga.Length)
                {
                    if (viga[iPos] == '=')
                    {
                        int count = 0;
                        while (iPos < viga.Length && viga[iPos] == '=')
                        {
                            count++;
                            iPos++;
                        }

                        long pesoSecuencia = (long)count * (count + 1) / 2;
                        pesoTotal += pesoSecuencia;

                        if (iPos < viga.Length && viga[iPos] == '*')
                        {
                            pesoTotal += 2 * pesoSecuencia;
                            iPos++;
                        }
                    }
                    else
                    {
                        // '*' sin largueros antes -> mal construida
                        Console.WriteLine("La viga está mal construida!");
                        goto Preguntar;
                    }
                }

                // 5. Comparar peso con la capacidad
                if (pesoTotal <= capacidad)
                    Console.WriteLine("La viga soporta el peso!");
                else
                    Console.WriteLine("La viga NO soporta el peso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        Preguntar:
            Console.Write("¿Desea continuar [S/N]?: ");
            response = Console.ReadLine()!.ToUpper();

            while (response != "S" && response != "N")
            {
                Console.WriteLine("Por favor ingrese 'S' para continuar o 'N' para salir.");
                response = Console.ReadLine()!.ToUpper();
            }

        } while (response == "S");

        Console.WriteLine("Game Over");
    }
}
