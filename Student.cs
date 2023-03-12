using System;
using System.Text.RegularExpressions;

namespace HW_09_03_23_params_ref_in_out
{
    public class Student
    {
        private int id;
        private string name;
        private string lastname;
        private string surname;
        private string phoneNumber;
        DateTime birthday;
        Address address;

        List<int> offsets = new List<int>() { };
        List<int> hometasks = new List<int>();
        List<int> exams = new List<int>();

        public Student(string name, string lastname, string surname, DateTime birthday, string phoneNumber, string city, string street, string homeNumber)
        {
            setName(name);
            setLastname(lastname);
            setSurname(surname);
            setPhoneNumber(phoneNumber);
            setBirthday(birthday);
            setAddress(city, street, homeNumber);
            fillingLists();
            id = new Random().Next(357943, 8357235);
        }
        public Student(string name, string lastname, string surname, DateTime birthday, string phoneNumber) :
            this(name, lastname, surname, birthday, phoneNumber, "None", "None", "None")
        { }
        public Student(string name, string lastname, string surname) :
            this(name, lastname, surname, new DateTime(1, 1, 1), "+38**********", "None", "None", "None")
        { }
        public Student() :
            this("None", "None", "None", new DateTime(1, 1, 1), "+38**********", "None", "None", "None")
        { }

        public void setName(string name) { this.name = name; }
        public void setLastname(string lastname) { this.lastname = lastname; }
        public void setSurname(string surname) { this.surname = surname; }
        public void setPhoneNumber(string phoneNumber)
        {
            string phoneRegexp = @"^\(\d{3}\)\d{3}\-\d{4}$";
            do
            {
                this.phoneNumber = phoneNumber;
            } while (!Regex.IsMatch(phoneNumber, phoneRegexp));
        }
        public void setBirthday(DateTime birthday) { this.birthday = birthday; }
        public void setAddress(string city, string street, string homeNumber) { this.address = new Address(city, street, homeNumber); }

        public int getId() { return id; }
        public string getName() { return this.name; }
        public string getLastname() { return this.lastname; }
        public string getSurname() { return this.surname; }
        public string getPhoneNumber() { return this.phoneNumber; }
        public string getBirthday() { return this.birthday.Date.ToString("d"); }
        public Address getAddress() { return this.address; }

        private void fillingLists()
        {
            for (int i = 0; i < 7; ++i)
            {
                offsets.Add(new Random().Next(6, 13));
                hometasks.Add(new Random().Next(2, 13));
                exams.Add(new Random().Next(6, 13));
            }
        }

        public List<int> getListOffsets() { return offsets; }
        public List<int> getListHometasks() { return hometasks; }
        public List<int> getListExams() { return exams; }

        public string getListOffsetsForToString() { return string.Join(" ", this.getListOffsets()); }
        public string getListHometasksForToString() { return string.Join(" ", this.getListHometasks()); }
        public string getListExamsForToString() { return string.Join(" ", this.getListExams()); }

        public override string ToString()
        {
            return ($"ID: {getId()}\n" +
                $"Student: {getLastname()} {getName()} {getSurname()}\n" +
                $"Birthday: {getBirthday()}\n" +
                $"Address: {getAddress()}\n" +
                $"Phone number: {getPhoneNumber()}\n" +
                $"Rating\n" +
                $"Scores offsets - {getListOffsetsForToString()}\n" +
                $"Scores hometasks - {getListHometasksForToString()}\n" +
                $"Scores exams - {getListExamsForToString()}\n");
        }
    }
}