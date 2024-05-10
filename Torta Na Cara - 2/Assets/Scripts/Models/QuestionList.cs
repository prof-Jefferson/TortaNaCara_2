using System.Collections.Generic;

[System.Serializable]
public class QuestionList
{
    public List<Question> questions;

    public QuestionList()
    {
        questions = new List<Question>();
    }
}
