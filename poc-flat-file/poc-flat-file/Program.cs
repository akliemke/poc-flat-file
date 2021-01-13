using FlatFiles;
using FlatFiles.TypeMapping;
using System;
using System.IO;
using System.Text;

namespace poc_flat_file
{
    class Program
    {
        static void Main(string[] args)
        {
            //FixedLengthSchema schema = new FixedLengthSchema();
            //schema.AddColumn(new StringColumn("First"), new Window(10) { FillCharacter = '@' });
            //schema.AddColumn(new StringColumn("Second"), new Window(10) { FillCharacter = '!' });
            //schema.AddColumn(new StringColumn("Third"), new Window(10) { FillCharacter = '$' });
            //FixedLengthOptions options = new FixedLengthOptions() { IsFirstRecordHeader = true };



            //StringWriter stringWriter = new StringWriter();
            //FixedLengthWriter writer = new FixedLengthWriter(stringWriter, schema, options);
            //writer.Write(new object[] { "Apple", "Grape", "Pear" });
            //writer.Write(new object[] { "1", "2", "3" });
            //Console.WriteLine(stringWriter.ToString());
         
            GerarModelo1(5);
            GerarModelo2(10);
        }

        public static void GerarModelo1(int tamanho)
        {
            var mapper = FixedLengthTypeMapper.DefineDynamic(typeof(Person));
            mapper.StringProperty("Name", tamanho).ColumnName("Name");
            mapper.Int32Property("IQ", tamanho).ColumnName("IQ");
            mapper.DateTimeProperty("BirthDate", 10).ColumnName("BirthDate").InputFormat("yyyyMMdd").OutputFormat("yyyyMMdd");
            //mapper.DecimalProperty("TopSpeed", 10).ColumnName("TopSpeed");
            var options = new FixedLengthOptions() { Alignment = FixedAlignment.RightAligned };
            var people = new[]
            {
                new Person() { Name = "John", IQ = null, BirthDate = new DateTime(1954, 10, 29), TopSpeed = 3.4m },
                new Person() { Name = "Susan", IQ = 132, BirthDate = new DateTime(1984, 3, 15), TopSpeed = 10.1m }
            };

            StringWriter writer = new StringWriter();
            mapper.Write(writer, people, options);
            string result = writer.ToString();

            using (StreamWriter writer2 = new StreamWriter($@"D:\teste\GerarModelo1_{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt"))
            {
                writer2.Write(result);
            }
        }
        public static void GerarModelo2(int tamanho)
        {
            var mapper = FixedLengthTypeMapper.DefineDynamic(typeof(Person));
            mapper.StringProperty("Name", tamanho).ColumnName("Name");
            mapper.Int32Property("IQ", tamanho).ColumnName("IQ");
            mapper.DateTimeProperty("BirthDate", 10).ColumnName("BirthDate").InputFormat("yyyyMMdd").OutputFormat("yyyyMMdd");
            mapper.DecimalProperty("TopSpeed", 10).ColumnName("TopSpeed");

            var people = new[]
            {
                new Person() { Name = "John", IQ = null, BirthDate = new DateTime(1954, 10, 29), TopSpeed = 3.4m },
                new Person() { Name = "Susan", IQ = 132, BirthDate = new DateTime(1984, 3, 15), TopSpeed = 10.1m }
            };

            StringWriter writer = new StringWriter();
            mapper.Write(writer, people);
            string result = writer.ToString();

            using (StreamWriter writer2 = new StreamWriter($@"D:\teste\GerarModelo2_{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt"))
            {
                writer2.Write(result);
            }

        }
    }
}
