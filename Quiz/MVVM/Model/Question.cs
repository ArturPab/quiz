namespace Quiz.MVVM.Model
{
    public class Question
    {
        public string QuestionContent { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public string CorrectAnswer { get; set; }

        public Question(string questionContent, string answerA, string answerB, string answerC, string answerD, string correctAnswer)
        {
            QuestionContent = questionContent;
            AnswerA = answerA;
            AnswerB = answerB;
            AnswerC = answerC;
            AnswerD = answerD;
            CorrectAnswer = correctAnswer;
        }
    }
}
