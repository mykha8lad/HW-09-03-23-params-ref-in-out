using Faker;
using System.Text.RegularExpressions;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;

namespace HW_09_03_23_params_ref_in_out
{
    public enum GroupMenu
    {
        GROUP_NAME = 1,
        GROUP_SPECIALIZATION,
        COURSE_NUMBER,
        GROUP_EXIT
    }
    public enum StudentMenu
    {
        STUDENT_NAME = 1,
        STUDENT_LASTNAME,
        STUDENT_SURNAME,
        STUDENT_PHONE_NUMBER,
        STUDENT_BIRTHDAY,
        STUDENT_ADDRESS,
        STUDENT_EXIT
    }

    public class Group
    {
        private const int studentsInGroup = 5;

        private string groupName;
        private string groupSpecialization;
        private Int16 courseNumber;

        public List<Student> students = new List<Student>();

        List<string> groupNames = new List<string>() { "P10", "P11", "P12", "P13", "P14", "P15" };
        List<string> groupSpecializations = new List<string>() { "C++", "JavaScript", "C", "C#", "Python", "Java", "Ruby", "PHP" };
        List<Int16> coursesNumber = new List<Int16>() { 1, 2, 3, 4, 5 };

        public Group()
        {
            createGroup(groupNames[new Random().Next(groupNames.Count)], groupSpecializations[new Random().Next(groupSpecializations.Count)], coursesNumber[new Random().Next(coursesNumber.Count)]);
        }
        public Group(List<Student> oldListStudents)
        {
            setGroupName(groupNames[new Random().Next(groupNames.Count)]);
            setGroupSpecialization(groupSpecializations[new Random().Next(groupSpecializations.Count)]);
            setCourseNumber(coursesNumber[new Random().Next(coursesNumber.Count)]);
            copyToThisListStudents(oldListStudents);
            oldListStudents.Clear();
        }
        public Group(Group group)
        {
            setGroupName(group.getGroupName());
            setGroupSpecialization(group.getGroupSpecialization());
            setCourseNumber(group.getCourseNumber());
            foreach (Student student in group.getListStudents())
            {
                students.Add(student);
            }
            //copyToThisListStudents(group.getListStudents());            
            group.setGroupName(null);
            group.setGroupSpecialization(null);
            group.setCourseNumber(0);
            group.getListStudents().Clear();
        }

        private void copyToThisListStudents(List<Student> oldListStudents)
        {
            this.students = new List<Student>(oldListStudents);
        }

        private void setGroupName(string groupName) { this.groupName = groupName; }
        private void setGroupSpecialization(string groupSpecialization) { this.groupSpecialization = groupSpecialization; }
        private void setCourseNumber(Int16 courseNumber) { this.courseNumber = courseNumber; }

        public string getGroupName() { return this.groupName; }
        public string getGroupSpecialization() { return this.groupSpecialization; }
        public Int16 getCourseNumber() { return this.courseNumber; }

        public void createGroup(string groupName, string groupSpecialization, Int16 courseNumber)
        {
            setGroupName(groupName);
            setGroupSpecialization(groupSpecialization);
            setCourseNumber(courseNumber);

            for (int student = 1; student <= studentsInGroup; student++)
            {
                string name = Faker.Name.First();
                string lastname = Faker.Name.Last();
                string surname = Faker.Name.Middle();
                string phoneRegexp = @"^\(\d{3}\)\d{3}\-\d{4}$";
                string phoneNumber;
                do
                {
                    phoneNumber = Faker.Phone.Number();
                } while (!Regex.IsMatch(phoneNumber, phoneRegexp));
                Random random = new Random();
                DateTime birthday = new DateTime(random.Next(2003, 2007), random.Next(1, 13), random.Next(1, 29));
                students.Add(new Student(name, lastname, surname, birthday, phoneNumber, Faker.Address.City(), Faker.Address.StreetName(), Faker.Address.ZipCode()));
            }
        }

        public List<Student> getListStudents() { return this.students; }

        public string getAllStudentsInfo()
        {
            students.Sort((firstStudent, secondStudent) => firstStudent.getLastname().CompareTo(secondStudent.getLastname()));
            return string.Join("\n\n", getListStudents());
        }

        public override string ToString()
        {
            return $"Group: {getGroupName()} Specialization: {getGroupSpecialization()} Course: {getCourseNumber()}\n" +
                $"--------------------------------------------------------------------------------\n" +
                $"{getAllStudentsInfo()}";
        }

        public void addStudentInGroup(Group group, Student student) { group.students.Add(student); }
        public void editData()
        {
            int userAnswer;
            bool flag = true;            

            while(flag)
            {
                Console.WriteLine("enter item\n1-edit group\n2-edit student info\n3-exit");
                do
                {
                    Console.Write("> ");
                    userAnswer = int.Parse(Console.ReadLine());
                } while (userAnswer < 1 || userAnswer > 3);

                switch (userAnswer)
                {
                    case 1:
                        ShowGroupMenu();
                        break;
                    case 2:
                        ShowStudentMenu();
                        break;
                    case 3:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Wrong item");
                        break;
                }    
            }
        }
        public void studentTransfer(Group group)
        {
            Console.WriteLine($"Введите id студента текущей группы ({getGroupName()}), которого собираетесь перевести в группу {group.getGroupName()}");
            int id = int.Parse(Console.ReadLine());

            foreach (Student student in students)
            {
                if (student.getId() == id)
                {
                    group.students.Add(student);
                    deleteStudent(student);
                    break;
                }
            }
        }

        private void deleteStudent(Student student) { students.Remove(student); }

        private void ShowStudentMenu()
        {
            int userAnswer;
            bool flag = true;
            Student st = null;

            Console.WriteLine($"Введите id студента текущей группы ({getGroupName()})");
            int id = int.Parse(Console.ReadLine());
           
            foreach (Student student in students)
            {
                if(student.getId() == id)
                {
                    st = student;
                    break;
                }
            }
            
            while (flag)
            {
                Console.WriteLine("Enter menu item\n1-name\n2-lastname\n3-surname\n4-phone number\n5-birthday\n6-address\n7-exit");
                do
                {
                    Console.Write("> ");
                    userAnswer = int.Parse(Console.ReadLine());
                } while (userAnswer < 1 || userAnswer > 7);

                switch (userAnswer)
                {
                    case (int)StudentMenu.STUDENT_NAME:
                        Console.WriteLine("Enter name student");
                        string stName;
                        do
                        {
                            Console.Write("> ");
                            stName = Console.ReadLine();
                        } while (String.IsNullOrEmpty(stName));
                        st.setName(stName);
                        break;
                    case (int)StudentMenu.STUDENT_LASTNAME:
                        Console.WriteLine("Enter lastname student");
                        string stLastName;
                        do
                        {
                            Console.Write("> ");
                            stLastName = Console.ReadLine();
                        } while (String.IsNullOrEmpty(stLastName));
                        st.setLastname(stLastName);
                        break;
                    case (int)StudentMenu.STUDENT_SURNAME:
                        Console.WriteLine("Enter surname student");
                        string stSurname;
                        do
                        {
                            Console.Write("> ");
                            stSurname = Console.ReadLine();
                        } while (String.IsNullOrEmpty(stSurname));
                        st.setSurname(stSurname);
                        break;
                    case (int)StudentMenu.STUDENT_PHONE_NUMBER:
                        Console.WriteLine("Enter phone student (xxx)xxx-xxxx");
                        string phoneNumber;
                        do
                        {
                            Console.Write("> ");
                            phoneNumber = Console.ReadLine();
                        } while (String.IsNullOrEmpty(phoneNumber));
                        st.setPhoneNumber(phoneNumber);
                        break;
                    case (int)StudentMenu.STUDENT_BIRTHDAY:
                        Console.WriteLine("Enter birthday xx.xx.xxxx");
                        DateTime birthday;
                        do
                        {
                            Console.Write("> ");
                            birthday = DateTime.Parse(Console.ReadLine());
                        } while (String.IsNullOrEmpty(birthday.ToString("d")));
                        st.setBirthday(birthday);
                        break;
                    case (int)StudentMenu.STUDENT_ADDRESS:
                        string city;
                        string street;
                        string homeNumber;

                        Console.WriteLine("Enter address");
                        do
                        {
                            Console.Write("City > ");
                            city = Console.ReadLine();
                            Console.Write("Street > ");
                            street = Console.ReadLine();
                            Console.Write("Home Number > ");
                            homeNumber = Console.ReadLine();
                        } while (String.IsNullOrEmpty(city) & String.IsNullOrEmpty(street) & String.IsNullOrEmpty(homeNumber));
                        st.setAddress(city, street, homeNumber);
                        break;
                    case (int)StudentMenu.STUDENT_EXIT:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Wrong item");
                        break;
                }
            }
        }
        private void ShowGroupMenu()
        {
            int userAnswer;
            bool flag = true;

            while (flag)
            {
                Console.WriteLine("Enter menu item\n1-name\n2-spec\n3-course number\n4-exit");
                do
                {
                    Console.Write("> ");
                    userAnswer = int.Parse(Console.ReadLine());
                } while (userAnswer < 1 || userAnswer > 4);

                switch (userAnswer)
                {
                    case (int)GroupMenu.GROUP_NAME:
                        Console.WriteLine("Enter group name");
                        string gName;
                        do
                        {
                            Console.Write("> ");
                            gName = Console.ReadLine();
                        } while (!groupNames.Contains(gName));
                        setGroupName(gName);
                        break;
                    case (int)GroupMenu.GROUP_SPECIALIZATION:
                        Console.WriteLine("Enter group spec");
                        string gSpec;
                        do
                        {
                            Console.Write("> ");
                            gSpec = Console.ReadLine();
                        } while (!groupSpecializations.Contains(gSpec));
                        setGroupSpecialization(gSpec);
                        break;
                    case (int)GroupMenu.COURSE_NUMBER:
                        Console.WriteLine("Enter course");
                        Int16 gCourse;
                        do
                        {
                            Console.Write("> ");
                            gCourse = Int16.Parse(Console.ReadLine());
                        } while (!coursesNumber.Contains(gCourse));
                        setCourseNumber(gCourse);
                        break;
                    case (int)GroupMenu.GROUP_EXIT:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Wrong item");
                        break;
                }
            }
        }

        public void deletingAllStudentPassSession()
        {
            students.RemoveAll(s => s.getListOffsets().Any(score => score < 7));
        }
        public void deleteFailedStudent()
        {
            double minAvg = double.MaxValue;
            Student failedStudent = null;
            foreach (Student student in students)
            {
                double avg = 0;
                avg += student.getListOffsets().Average() + student.getListHometasks().Average() + student.getListExams().Average();

                if (avg < minAvg)
                {
                    minAvg = avg;
                    failedStudent = student;
                }
            }
            Console.WriteLine($"Отчисление студента {failedStudent.getName()} ({failedStudent.getId()})");
            deleteStudent(failedStudent);
        }
    }
}
