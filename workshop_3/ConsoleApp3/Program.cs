using shared;

class StrongestBeam
{
    static void Main()
    {
        while (true)
        {
            try
            {
                Console.Write("Ingrese la viga: ");
                string viga = Console.ReadLine() ?? "";

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

                bool simboloInvalido = false;
                for (int i = 1; i < viga.Length; i++)
                {
                    if (viga[i] != '=' && viga[i] != '*')
                    {
                        simboloInvalido = true;
                        break;
                    }
                }

                if (simboloInvalido || (viga.Length > 1 && viga[1] == '*'))
                {
                    Console.WriteLine("La viga está mal construida!");
                    continue;
                }

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

                        while (iPos < viga.Length && viga[iPos] == '*')
                        {
                            pesoTotal += 2 * pesoSecuencia;
                            iPos++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("La viga está mal construida!");
                        pesoTotal = -1;
                        break;
                    }
                }

                if (pesoTotal < 0)
                {
                    continue;
                }

                if (pesoTotal <= capacidad)
                    Console.WriteLine("La viga soporta el peso!");
                else
                    Console.WriteLine("La viga NO soporta el peso!");
            }
            catch
            {
                Console.WriteLine("La viga está mal construida!");
            }
        }
    }
}
