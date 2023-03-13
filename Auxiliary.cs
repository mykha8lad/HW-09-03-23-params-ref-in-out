using System;

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

    public class RandomDataForGroup
    {
        public List<string> groupNames = new List<string>() { "P10", "P11", "P12", "P13", "P14", "P15" };
        public List<string> groupSpecializations = new List<string>() { "C++", "JavaScript", "C", "C#", "Python", "Java", "Ruby", "PHP" };
        public List<int> coursesNumber = new List<int>() { 1, 2, 3, 4, 5 };
    }
}
