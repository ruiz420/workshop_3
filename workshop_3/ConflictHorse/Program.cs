using shared;

class Program
{
    static void Main()
    {
        string response = string.Empty;

        do
        {
            try
            {
                var name = ConsoleExtensions.Getstring("Ingrese las ubicaciones de los caballos (por ejemplo: B7,C5,E2,H7):");
                var positions = name.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries); // array 

                var caballos = positions.Select(pos => new Position(pos)).ToList();

                foreach (var horse in caballos) // conflict
                {
                    var conflicts = new List<string>();

                    
                    foreach (var otherHorse in caballos.Where(h => h != horse))
                    {
                        if (IsConflict(horse, otherHorse))
                        {
                            conflicts.Add(otherHorse.ToString());
                        }
                    }

                    
                    Console.WriteLine($"El Caballo en {horse} => Conflicto con {string.Join(", ", conflicts.Any() ? conflicts : new List<string> { "ninguno" })}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Preguntar si desea continuar
            Console.WriteLine("Desea Continuar [S/N]?: ");
            response = Console.ReadLine()!.ToUpper();

            while (response != "S" && response != "N")
            {
                Console.WriteLine("Por favor ingrese 'S' para continuar o 'N' para salir.");
                response = Console.ReadLine()!.ToUpper();
            }

        } while (response == "S");

        Console.WriteLine("Game Over");
    }

    public static bool IsConflict(Position horse1, Position horse2)
    {
        int rowDiff = Math.Abs(horse1.Row - horse2.Row);
        int colDiff = Math.Abs(horse1.Column - horse2.Column);

        return (rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2);
    }

    public class Position
    {
        public int Row 
        { 
            get; 
            set; 
        }
        public int Column 
        { 
            get; 
            set; 
        }

        public Position(string pos)
        {
            Column = pos[0] - 'A'; // A=0, B=1, C=2, ...
            Row = 8 - int.Parse(pos[1].ToString()); // Convierte fila (8-1, 8-2, ...)
        }

        public override string ToString()
        {
            // Representar la posición en formato "Algebraic Notation" (por ejemplo, "B7")
            string[] columnas = new string[] { "A", "B", "C", "D", "E", "F", "G", "H" };
            return columnas[Column] + (8 - Row);
        }
    }
}
