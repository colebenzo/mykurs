using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfficiencyMark.Models
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationContext db)
        {
            db.Database.EnsureCreated();

            if (db.Employees.Any() && db.ListOfIndicators.Any() && db.AchievementsOfAnEmployee.Any())
            {
                return;
            }

            Random randObj = new Random(1);
           
           


            string[] surname = { "Иванов", "Петров", "Сидоров", "Новиков" };
            int count_surname_voc = surname.GetLength(0);
            string[] name = { "Иван", "Петр", "Николай", "Владимир" };
            int count_name_voc = name.GetLength(0);
            string[] middleName = { "Валерьевич", "Олегович", "Владимирович", "Иванович" };
            int count_middleName_voc = middleName.GetLength(0);
            string[] position = { "Водитель", "Стажер" };
            int count_position_voc = position.GetLength(0);
            for (int ID = 1; ID <= 10; ID++)
            {
                DateTime today = DateTime.Now.Date;
                DateTime date = today.AddDays(-ID);
                db.Employees.Add(new Employees
                {
                    Surname = surname[randObj.Next(count_surname_voc)],
                    Name = name[randObj.Next(count_name_voc)],
                    MiddleName = middleName[randObj.Next(count_middleName_voc)],
                    Phone = randObj.Next(100000, 999999),
                    YearOfBirth = date
                });
            }
            db.SaveChanges();


            string[] indicator = { "показатель1", "показатель2", "показатель3", "показатель4" };
            int count_indicatort_voc = indicator.GetLength(0);
            for (int ID = 1; ID <= 10; ID++)
            {
                db.ListOfIndicators.Add(new ListOfIndicators
                {
                    EmployeeId = randObj.Next(1, 10),
                    NameOfTheIndicator = indicator[randObj.Next(count_indicatort_voc)]
                });
            }
            db.SaveChanges();

            string[] achievement = { "достижение1", "достижение2", "достижение3", "достижение4" };
            int count_achievement_voc = achievement.GetLength(0);
            for (int ID = 1; ID <= 10; ID++)
            {
                db.AchievementsOfAnEmployee.Add(new AchievementsOfAnEmployee
                {
                    EmployeeId = randObj.Next(1, 10),
                    NameOfAchievement = achievement[randObj.Next(count_achievement_voc)]
                });
            }
            db.SaveChanges();

        }
    }
}
