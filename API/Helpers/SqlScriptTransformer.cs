namespace API.Helpers
{
    public class SqlScriptTransformer
    {
        public SqlScriptTransformer() { }

        public async Task RegionsScriptTransformer(Dictionary<int, String> countries)
        {
            string rutaEntrada = @"C:\Users\matias\source\repos\proyecto-final-catalogo-ventas\API\implementations\Infrastructure\Data\Regions.sql";
            string rutaSalida = @"C:\Users\matias\Desktop\transformado.sql";

            try
            {
                using (StreamReader lector = new StreamReader(rutaEntrada))
                using (StreamWriter escritor = new StreamWriter(rutaSalida))
                {
                    string linea;
                    while ((linea = lector.ReadLine()) != null)
                    {
                        // Aquí puedes aplicar la lógica de transformación a cada línea
                        string lineaTransformada = TransformarLinea(linea);
                        escritor.WriteLine(lineaTransformada);
                        Console.WriteLine(lineaTransformada);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar el archivo: {ex.Message}");
            }
        }

        static string TransformarLinea(string line)
        {
            line = line.Trim();

            if (line.StartsWith("(") && line.EndsWith("),") || line.EndsWith(");"))
            {
                // Eliminar los paréntesis y la coma final
                string content = line.Substring(1, line.Length - 3);
                string[] fields = content.Split(',');

                if (fields.Length >= 5)
                {
                    string nombreCiudad = fields[1].Trim();
                    string nombrePais = fields[4].Trim();

                    string nuevaLinea = $"(NEWID(), {nombreCiudad}, {nombrePais}, @Now, @Now),";
                    return nuevaLinea;
                }
            }

            return line;
        }

    }
}
