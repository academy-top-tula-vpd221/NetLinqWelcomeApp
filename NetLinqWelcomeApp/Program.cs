using NetLinqWelcomeApp;

List<Employee> employees = new()
{
    new(){ Name = "Bobby", Age = 24, Phone = "3243234253" },
    new(){ Name = "Joe", Age = 33, Phone = "1111111111" },
    new(){ Name = "Sam", Age = 19, Phone = "4534534553" },
    new(){ Name = "Jimmy", Age = 42, Phone = "6786678678" },
    new(){ Name = "Leopold", Age = 29, Phone = "6267667567" },
    new(){ Name = "Bob", Age = 19, Phone = "3243234253" },
    new(){ Name = "Jotheph", Age = 26, Phone = "1111111111" },
    new(){ Name = "Sammy", Age = 34, Phone = "4534534553" },
    new(){ Name = "Jim", Age = 27, Phone = "6786678678" },
    new(){ Name = "Leo", Age = 32, Phone = "6267667567" },
};

List<Company> companies = new()
{
    new(){ Title = "Yandex", Employees = new(){ new Employee() { Name = "Tim", Age = 23 }, new Employee() { Name = "Sam", Age = 45} } },
    new(){ Title = "Ozon", Employees = new(){ new Employee() { Name = "Leo", Age = 35 }, new Employee() { Name = "Jim", Age = 21}, new Employee() { Name = "Ann", Age = 19 } } },
    new(){ Title = "Mail Group", Employees = new(){ new Employee() { Name = "Max", Age = 38 } } },
};

List<Shape> shapes = new()
{
    new Rectangle(){ Name = "Rectangle 1" },
    new Shape(){ Name = "Shape 1" },
    new Rectangle(){ Name = "Rectangle 2" },
    new Circle(){ Name = "Circle 1" },
    new Shape(){ Name = "Shape 2" },
    new Triangle(){ Name = "Triangle 1" },
};




void LinqOrderBy()
{
    var emplsSortNameO = from e in employees
                         orderby e.Name descending
                         select e;

    foreach (var e in emplsSortNameO)
        Console.WriteLine($"{e.Name} {e.Age}");
    Console.WriteLine();

    var emplsSortNameM = employees.OrderByDescending(e => e.Name).ToList();
    foreach (var e in emplsSortNameM)
        Console.WriteLine($"{e.Name} {e.Age}");
    Console.WriteLine();

    var emplsSortNameAgeO = from e in employees
                            orderby e.Name ascending, e.Age descending
                            select e;

    foreach (var e in emplsSortNameAgeO)
        Console.WriteLine($"{e.Name} {e.Age}");
    Console.WriteLine();


    var emplsSortNameAgeM = employees.OrderBy(e => e, new EmployeNameLengthComparer())
                                     .ThenByDescending(e => e.Age);

    foreach (var e in emplsSortNameAgeM)
        Console.WriteLine($"{e.Name} {e.Age}");
    Console.WriteLine();
}


void LinqWhere()
{
    var shapesR = from s in shapes
                      //where s.GetType() == typeof(Shape)
                  where (s is Rectangle)
                  select s;
    foreach (Shape s in shapesR)
        Console.WriteLine(s.Name);
    Console.WriteLine();

    var shapesRM = shapes.OfType<Shape>();
    foreach (Shape s in shapesRM)
        Console.WriteLine(s.Name);
    Console.WriteLine();


    //var empls30O = from e in employees
    //              where e.Age > 30
    //              select e;
    //var empls30M = employees.Where(e => e.Age > 30);

    var emplsComp30O = from c in companies
                       from empls in c.Employees
                       where empls.Age > 30
                       select new
                       {
                           Name = empls.Name,
                           Age = empls.Age,
                           Company = c.Title
                       };

    foreach (var e in emplsComp30O)
        Console.WriteLine($"{e.Name} {e.Age} {e.Company}");
    Console.WriteLine();

    var emplsComp30M = companies.SelectMany(c => c.Employees,
                                            (c, empls) => new
                                            {
                                                Name = empls.Name,
                                                Age = empls.Age,
                                                Company = c.Title
                                            })
                                .Where(e => e.Age > 30)
                                .Select(e => e);

    foreach (var e in emplsComp30M)
        Console.WriteLine($"{e.Name} {e.Age} {e.Company}");
    Console.WriteLine();
    //
}
void LinqSelectMany()
{
    var employesAllO = from c in companies
                       from empls in c.Employees
                       select new
                       {
                           Name = empls.Name,
                           Company = c.Title
                       };

    foreach (var e in employesAllO)
        Console.WriteLine($"{e.Name} {e.Company}");
    Console.WriteLine();

    var employesAllM = companies.SelectMany(c => c.Employees,
                                            (c, empls) => new
                                            {
                                                Name = empls.Name,
                                                Company = c.Title
                                            });
    foreach (var e in employesAllM)
        Console.WriteLine($"{e.Name} {e.Company}");
    Console.WriteLine();
}
void LinqSelectSimple()
{
    //var employeesNamesAgesO = from e in employees
    //                          where e.Age > 30
    //                          select new 
    //                          { 
    //                              Name = e.Name,
    //                              Age = e.Age,
    //                          };

    //foreach(var e in employeesNamesAgesO)
    //    Console.WriteLine($"{e.Name} {e.Age}");
    //Console.WriteLine();

    //var employeesNamesAgesM = employees.Where(e => e.Age > 30)
    //                                   .Select(e => new
    //{
    //    Name = e.Name,
    //    Age = e.Age,
    //});

    //foreach (var e in employeesNamesAgesM)
    //    Console.WriteLine($"{e.Name} {e.Age}");
    //Console.WriteLine();


    var emplCompO = from e in employees
                    from c in companies
                    select new
                    {
                        Name = e.Name,
                        Company = c.Title
                    };

    foreach (var item in emplCompO)
        Console.WriteLine($"{item.Name} {item.Company}");
}
void LinqWelcome()
{
    string[] strs = { "Tom", "Bobby", "Sam", "Peet", "Leo", "Mike" };

    List<string> namesBig = new List<string>();
    foreach (string str in strs)
        if (str.Length > 3)
            namesBig.Add(str);

    foreach (string str in namesBig)
        Console.WriteLine(str);
    Console.WriteLine();

    var namesBigLinqO = from n in strs
                        where n.Length > 3
                        orderby n
                        select n;

    foreach (string str in namesBigLinqO)
        Console.WriteLine(str);
    Console.WriteLine();

    var namesBigLinqM = strs.Where(s => s.Length > 3)
                            .OrderBy(s => s);

    foreach (string str in namesBigLinqM)
        Console.WriteLine(str);
    Console.WriteLine();
}