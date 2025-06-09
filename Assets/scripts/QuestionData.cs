[System.Serializable]
public class Question
{
    public string question;
    public string[] options;
    public string correct_answer;
}

[System.Serializable]
public class Category
{
    public Question[] IstorieȘiGeografie;
    public Question[] ȘtiințăȘiTehnologie;
    public Question[] ArtăȘiCultură;
}
