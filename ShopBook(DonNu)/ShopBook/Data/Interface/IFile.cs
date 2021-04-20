namespace ShopBook.Data.Interface
{

    interface IFile<T>
    {
        T[] Search(string[] mass);
        void NewObject(T objectt);
        void Deleting_Object(T objectt);
        void Deleting_Object(string[] mass);
        T[] Load_all();
        bool Duplicate_search(string[] mass);
    }
}
