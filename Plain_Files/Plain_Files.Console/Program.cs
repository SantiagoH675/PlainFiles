using PlainFiles.Core;

Console.Write("Digite el nombre de la lista (por defecto 'people'): ");
var listName = Console.ReadLine();
if (string.IsNullOrEmpty(listName))
{
    listName = "people";
}

var helper = new NugetCsvHelper();
var people = helper.Read($"{listName}.csv").ToList();
foreach (var person in people)
{
    Console.WriteLine($"ID: {person.Id}, Nombre: {person.Name}, Edad: {person.Age}");
}
int nextId = people.Count > 0 ? people.Max(p => p.Id) + 1 : 1;
string option;

do
{
    option = MyMenu();
    Console.WriteLine();

    switch (option)
    {
        case "1":
            AddPerson();
            break;

        case "2":
            ListPeople();
            break;

        case "3":
            SaveFile(people, listName);
            Console.WriteLine("Archivo guardado.");
            break;

        case "4":
            DeletePerson();
            break;

        case "5":
            SortData();
            break;

        case "0":
            Console.WriteLine("Saliendo...");
            break;

        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
} while (option != "0");
void AddPerson()
{
    Console.Write("Digite el nombre: ");
    var name = Console.ReadLine() ?? "";

    Console.Write("Digite la edad: ");
    int.TryParse(Console.ReadLine(), out int age);

    people.Add(new Person
    {
        Id = nextId++,
        Name = name,
        Age = age
    });

    Console.WriteLine("Persona agregada.");
}
void ListPeople()
{
    Console.WriteLine("ID | Nombre | Edad");

    foreach (var p in people)
    {
        Console.WriteLine($"{p.Id} | {p.Name} | {p.Age}");
    }
}
void DeletePerson()
{
    Console.Write("Digite el nombre de la persona a eliminar: ");
    var name = Console.ReadLine();

    var matches = people
        .Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
        .ToList();

    if (matches.Count == 0)
    {
        Console.WriteLine("No se encontraron personas con ese nombre.");
        return;
    }

    for (int i = 0; i < matches.Count; i++)
    {
        Console.WriteLine($"ID local {i}: {matches[i].Id} - {matches[i].Name} ({matches[i].Age})");
    }

    int index;
    do
    {
        Console.Write("Seleccione el ID local para borrar (-1 para cancelar): ");
        int.TryParse(Console.ReadLine(), out index);
    }
    while (index < -1 || index >= matches.Count);

    if (index == -1)
    {
        Console.WriteLine("Cancelado.");
        return;
    }

    var toRemove = matches[index];
    people.Remove(toRemove);

    Console.WriteLine("Persona eliminada.");
}
void SortData()
{
    Console.Write("Ordenar por: 0.Nombre, 1.Edad, 2.ID: ");
    int.TryParse(Console.ReadLine(), out int order);

    Console.Write("Tipo: 0.Ascendente, 1.Descendente: ");
    int.TryParse(Console.ReadLine(), out int type);

    people.Sort((a, b) =>
    {
        int cmp = order switch
        {
            0 => string.Compare(a.Name, b.Name, true),
            1 => a.Age.CompareTo(b.Age),
            2 => a.Id.CompareTo(b.Id),
            _ => 0
        };

        return type == 0 ? cmp : -cmp;
    });

    Console.WriteLine("Datos ordenados.");
}
string MyMenu()
{
    Console.WriteLine();
    Console.WriteLine("1. Adicionar");
    Console.WriteLine("2. Mostrar");
    Console.WriteLine("3. Grabar");
    Console.WriteLine("4. Eliminar");
    Console.WriteLine("5. Ordenar");
    Console.WriteLine("0. Salir");
    Console.Write("Seleccione una opción: ");
    return Console.ReadLine() ?? "";
}

void SaveFile(List<Person> people, string listName)
{
    helper.Write($"{listName}.csv", people);
}