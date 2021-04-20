using System.Collections.Generic;
using System.IO;
using ShopBook.Data.Interface;
using ShopBook.Entities.Users;
using ShopBook.Data.FileOperation;


namespace ShopBook.Data.FilePeopleGrup
{
    class FilePeople : Function_elements_FP, IFile<People>
    {
        public FilePeople()
        {
            Pathnow = Path; PathMapnow = PathAmounts;
        }
        public FilePeople(string Action)
        {
            if (Action == "Сheck")
            {
                Pathnow = Path;
                PathMapnow = PathAmounts;
                if (File.Exists(Pathnow) == false || File.Exists(PathMapnow) == false)
                {
                    FileStream file = File.Create(Pathnow);
                    file.Close();
                    file = File.Create(PathMapnow);
                    file.Close();
                    firstcreation = true;
                }
                else
                {
                    firstcreation = false;
                }
            }
        }
        public People[] Search(string[] mass)
        {
            List<int> massrez = Map_analysis(mass);
            return Loading(massrez);
        }
        public void NewObject(People objectt)
        {
            Adding_database_people(objectt);
            Changing_total_number_records("Number of users", "+1");
        }
        public void Deleting_Object(People objectt)
        {
            string[] mass = objectt.get_Login_Password();
            if(Delete_database(Map_analysis(mass)));
            {
                Delete_Map(mass);
                Changing_total_number_records("Number of users", "-1");
            }
        }
        public void Deleting_Object(string[] mass)
        {
            if (Delete_database(Map_analysis(mass))) ;
            {
                Delete_Map(mass);
                Changing_total_number_records("Number of users", "-1");
            }
        }
        public int Total_number_records()
        {
            return Changing_total_number_records("Number of users", "get");
        }

        public People[] Load_all()
        {
            List<int> massrez = All_Map();
            return Loading(massrez);
        }
        private People[] Loading (List<int> massrez)
        {
            if (massrez == null)
            {
                return null;
            }
            People[] rez = new People[massrez.Count / 2];
            int flag = 0;
            for (int i = 0; i < massrez.Count; i = i + 2)
            {
                FileStream stream = new FileStream(Path, FileMode.Open, FileAccess.Read);
                byte[] dataB = Encryption.File_decryption_object(stream, massrez[i], massrez[i + 1]);
                rez[flag] = (People)FileOperation.Byte.ByteArrayToObject(dataB);
                flag++;
                stream.Close();
            }
            return rez;
        }
        public bool Duplicate_search(string[] mass)
        {
            if (firstcreation == false)
            {
                if (Search(mass) != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
