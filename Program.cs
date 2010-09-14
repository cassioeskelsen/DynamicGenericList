using System;
using System.Collections;
using System.Collections.Generic;

namespace Sample
{
    /// <summary>
    /// Instantiates a generic list at runtime and allows  create a new instance of the generic type of the list
    /// Created by : Cassio Rogerio Eskelsen ( eskelsen _ gmail.com)
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Type t;

            while (true)
            {
                Console.WriteLine("Enter a type name (with namespace), like  Sample.Car, \n Sample.Motorcycle, System.TimeSpan");
                Console.WriteLine("The type must have a parameterless constructor");
                Console.WriteLine("Enter  \"exit\" to finish");
                Console.Write("> ");
                string tipoDigitado = Console.ReadLine();
                if (tipoDigitado.Equals("exit"))
                {
                    break;
                }
                Type tipoConvertido = Type.GetType(tipoDigitado, false, true);
                if (tipoConvertido == null)
                {
                    Console.WriteLine("Type not found");
                }
                else
                {
                    // create the list definition with the type informed                    
                    t = typeof(List<>).MakeGenericType(tipoConvertido);
                    // create a instance of the list above defined
                    IList listaConcreta = (IList)Activator.CreateInstance(t);
                    Type type = t.GetGenericArguments()[0];
                    try
                    {
                        // create a instance of the list type
                        object o = Activator.CreateInstance(type);
                        Console.WriteLine("\n\nCreated instance of type {0}. Properties:", o.GetType().FullName);
                        foreach (var p in o.GetType().GetProperties())
                        {
                            Console.WriteLine(p.Name + ", of type  " + p.PropertyType);
                        }

                        // add the "dynamic" object to list
                        listaConcreta.Add(o);

                    }
                    catch (MissingMethodException)
                    {
                        Console.WriteLine("the type {0} don't have a parameterless constructor", type.FullName);
                    }
                }
                Console.WriteLine("\n\nPress any key ...");
                Console.ReadLine();
                Console.Clear();
            }
        }

    }

    public class Car
    {
        public int Id { get; set; }
        public string Plate { get; set; }
    }

    public class Motorcycle
    {
        public int Id { get; set; }
        public string Brand { get; set; }
    }

}
