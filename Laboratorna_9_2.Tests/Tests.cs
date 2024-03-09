using NUnit.Framework;

namespace Laboratorna_9_2.Tests;

[TestFixture]
public class Tests
{
    [Test]
    public void Print()
    {
        StudentLevel[] students =
        {
            new StudentLevel
            {
                StudentNumber = 1, LastName = "Лисяк", Course = 1, Specialization = Specialization.Informatics,
                SubjectMarks = new Marks { Physics = 5, Mathematics = 5 },
                AdditionalMarks = new AdditionalMarks { NumericalMethods = 5 }
            }
        };

        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);

            Program.Print(students);

            var result = sw.ToString();

            Assert.IsTrue(result.Contains(
                "| \u2116 | Прізвище | Курс | Спеціалізація | Фізика | Математика | Програмування | Чисельні Методи | Педагогіка |\n"));
            Assert.IsTrue(
                result.Contains(
                    "|  1 | Лисяк      |    1 | Informatics    |      5 |         5 |               5 |\n"));

            Console.SetOut(Console.Out);
        }
    }

    [Test]
    public void Create()
    {
        StudentLevel[] students = new StudentLevel[1];

        using (var sr = new StringReader("Лисяк\n2\n0\n4\n5\n5"))
        using (var sw = new StringWriter())
        {
            Console.SetIn(sr);
            Console.SetOut(sw);
            Program.Create(students);

            Assert.AreEqual("Лисяк", students[0].LastName);
            Assert.AreEqual(2, students[0].Course);
            Assert.AreEqual(Specialization.ComputerScience, students[0].Specialization);
            Assert.AreEqual(4, students[0].SubjectMarks.Physics);
            Assert.AreEqual(5, students[0].SubjectMarks.Mathematics);
            Assert.AreEqual(5, students[0].AdditionalMarks.Programming);

            Console.SetOut(Console.Out);
        }
    }

    [Test]
    public void ComputeAverageMarks()
    {
        StudentLevel[] students =
        {
            new StudentLevel
            {
                SubjectMarks = new Marks { Physics = 4, Mathematics = 5 },
                AdditionalMarks = new AdditionalMarks { Programming = 3 }
            }
        };

        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);

            Program.ComputeAverageMarks(students);

            var result = sw.ToString().Trim().Replace(" ", "");

            Assert.AreEqual("Середнійбалстудента:4,00", result);
        }
    }

    [Test]
    public void CountStudentsWithHighPhysicsMarks()
    {
        StudentLevel[] students =
        {
            new StudentLevel
            {
                SubjectMarks = new Marks { Physics = 5, Mathematics = 5 },
                AdditionalMarks = new AdditionalMarks { Programming = 4 }
            }
        };

        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);

            Program.CountStudentsWithHighPhysicsMarks(students);

            var result = sw.ToString().Trim();

            Assert.AreEqual("Кількість студентів з високими оцінками з фізики: 1", result);
        }
    }

    [Test]
    public void Sort()
    {
        StudentLevel[] students =
        {
            new StudentLevel
            {
                LastName = "Лисяк", Course = 2, Specialization = Specialization.ComputerScience,
                SubjectMarks = new Marks { Physics = 5, Mathematics = 4 },
                AdditionalMarks = new AdditionalMarks { Programming = 5 }
            },
            new StudentLevel
            {
                LastName = "Римар", Course = 1, Specialization = Specialization.Informatics,
                SubjectMarks = new Marks { Physics = 4, Mathematics = 5 },
                AdditionalMarks = new AdditionalMarks { NumericalMethods = 3 }
            },
            new StudentLevel
            {
                LastName = "Дурибаба", Course = 2, Specialization = Specialization.ComputerScience,
                SubjectMarks = new Marks { Physics = 3, Mathematics = 4 },
                AdditionalMarks = new AdditionalMarks { Programming = 4 }
            }
        };

        Program.Sort(students);

        Assert.AreEqual("Римар", students[0].LastName);
        Assert.AreEqual("Дурибаба", students[1].LastName);
        Assert.AreEqual("Лисяк", students[2].LastName);
    }

    [Test]
    public void IndexSort()
    {
        StudentLevel[] students =
        {
            new StudentLevel
            {
                LastName = "Лисяк", Course = 2, Specialization = Specialization.ComputerScience,
                SubjectMarks = new Marks { Physics = 5, Mathematics = 4 },
                AdditionalMarks = new AdditionalMarks { Programming = 5 }
            },
            new StudentLevel
            {
                LastName = "Римар", Course = 1, Specialization = Specialization.Informatics,
                SubjectMarks = new Marks { Physics = 4, Mathematics = 5 },
                AdditionalMarks = new AdditionalMarks { NumericalMethods = 3 }
            },
            new StudentLevel
            {
                LastName = "Дурибаба", Course = 2, Specialization = Specialization.ComputerScience,
                SubjectMarks = new Marks { Physics = 3, Mathematics = 4 },
                AdditionalMarks = new AdditionalMarks { Programming = 4 }
            }
        };

        Program.IndexSort(students);

        Assert.AreEqual("Лисяк", students[0].LastName);
        Assert.AreEqual("Римар", students[1].LastName);
        Assert.AreEqual("Дурибаба", students[2].LastName);
    }

    [Test]
    public void BinarySearch()
    {
        StudentLevel[] students =
        {
            new StudentLevel
            {
                LastName = "Лисяк", Course = 2, Specialization = Specialization.ComputerScience,
                SubjectMarks = new Marks { Physics = 5, Mathematics = 4 },
                AdditionalMarks = new AdditionalMarks { Programming = 5 }
            },
        };
        Program.IndexSort(students);
        int expectedIndex = 1;
        int result = Program.BinarySearch(students, "Лисяк", Specialization.ComputerScience, 5) + 1;
        Assert.AreEqual(expectedIndex, result, $"Expected index: {expectedIndex}, Actual result: {result}");
    }
}