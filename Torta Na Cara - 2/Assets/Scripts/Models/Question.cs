using System.Collections.Generic;

[System.Serializable]
public class Question
{
    public string question;       // Texto da pergunta
    public List<string> options;  // Lista de opções
    public int answer;            // Índice da resposta correta

    // Construtor
    public Question(string question, List<string> options, int answer)
    {
        this.question = question;
        this.options = options;
        this.answer = answer;
    }
}
