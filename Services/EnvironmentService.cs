namespace CarRentalSystem.Services
{
    public class EnvironmentService
    {
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Env File not found");
                return;
            }

            foreach (var line in File.ReadAllLines(filePath))
            {
                //Console.WriteLine(line);
                var parts = line.Split(
                    ':',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;
                Console.WriteLine($"{parts[0]} : {parts[1]}");
                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }
}
