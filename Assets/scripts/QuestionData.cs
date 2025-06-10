using System;
using System.Collections.Generic;

[Serializable]
public class QuestionItem
{
    public string question;
    public List<string> options;
    public string correct_answer;
}

[Serializable]
public class QuestionCategory
{
    public List<QuestionItem> IstorieșiGeografie;
}
