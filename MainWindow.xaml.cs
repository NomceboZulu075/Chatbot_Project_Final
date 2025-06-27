using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chatbot_Project_FinalPart3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Enhanced Cybersecurity Awareness Chatbot with NLP Simulation
    /// Now featuring advanced natural language processing capabilities!
    /// </summary>
    public partial class MainWindow : Window
    {
        // Task class to store task information
        public class taskInformation
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? ReminderDate { get; set; }
            public bool IsCompleted { get; set; }
            public DateTime CreatedDate { get; set; }

            public override string ToString()
            {
                string status = IsCompleted ? "[COMPLETED]" : "[PENDING]";
                string reminder = ReminderDate.HasValue ? $" (Reminder: {ReminderDate.Value:MM/dd/yyyy})" : "";
                return $"{status} {Title} - {Description}{reminder}";
            }//end of ToString method
        }//end of taskInformation method

        //A List to store all inforamtion of the tasks
        private List<taskInformation> cyberTasks = new List<taskInformation>();

        // Activity log for tracking actions
        private List<string> activityLog = new List<string>();

        // Quiz System Variables
        public class QuizQuestion
        {
            public string Question { get; set; }
            public List<string> Options { get; set; }
            public int CorrectAnswer { get; set; }
            public string Explanation { get; set; }
            public bool IsTrueFalse { get; set; }
        }//end of quizquestion class

        // Activity Log Entry Class 
        public class ActivityLogEntry
        {
            public DateTime Timestamp { get; set; }
            public string Action { get; set; }
            public string Category { get; set; }
            public string Details { get; set; }

            public override string ToString()
            {
                return $"{Timestamp:MM/dd/yyyy HH:mm} - {Action}";
            }
        }

        private List<QuizQuestion> quizQuestions = new List<QuizQuestion>();
        private int currentQuestionIndex = 0;
        private int quizScore = 0;
        private bool isQuizActive = false;
        private int totalQuizzesTaken = 0;
        private int bestScore = 0;

        // NLP SIMULATION ENHANCEMENT - Advanced Natural Language Processing Variables
        // These dictionaries help the chatbot understand different ways users express the same intent

        private Dictionary<string, List<string>> intentKeywords;
        private Dictionary<string, List<string>> synonymDictionary;
        private Dictionary<string, string> commonTypos;
        private List<string> conversationalPhrases;
        private int nlpInteractionCount = 0; // Track how many times NLP helped understand user intent
        private List<ActivityLogEntry> enhancedActivityLog = new List<ActivityLogEntry>();
        private const int MAX_LOG_DISPLAY = 10; // Show last 10 actions by default


        public MainWindow()
        {
            InitializeComponent();
            InitializeQuizQuestions(); // Initialize quiz system!
            InitializeNLPComponents(); // Initialize NLP system!
            AddToActivityLog("Cybersecurity Awareness Chatbot started");
            UpdateStatistics();
            ShowWelcomeMessage();
        }//end of MainWindow constructor

        // NLP ENHANCEMENT: Initialize Natural Language Processing components
        // This method sets up dictionaries to help understand user intent better
        private void InitializeNLPComponents()
        {

            // Intent keywords - different ways users might express the same intent
            intentKeywords = new Dictionary<string, List<string>>
            {
                ["add_task"] = new List<string>
                {
                    "add task", "create task", "new task", "make task", "set task",
                    "add reminder", "create reminder", "remind me", "set reminder",
                    "help me remember", "schedule", "plan", "organize", "todo",
                    "i need to", "i should", "i want to", "can you remind"
                },
                ["show_tasks"] = new List<string>
                {
                    "show tasks", "view tasks", "list tasks", "my tasks", "see tasks",
                    "what tasks", "task list", "display tasks", "current tasks",
                    "show my list", "what do i need to do", "whats on my list"
                },
                ["start_quiz"] = new List<string>
                {
                    "start quiz", "take quiz", "quiz me", "begin quiz", "test me",
                    "challenge me", "game", "play", "questions", "test knowledge",
                    "mini-game", "cybersecurity test", "security quiz"
                },
                ["help"] = new List<string>
                {
                    "help", "commands", "what can you do", "how to use", "instructions",
                    "guide me", "assist", "support", "tutorial", "show me"
                },
                ["activity_log"] = new List<string>
                {
                    "activity log", "what have you done", "show log", "recent actions",
                    "history", "summary", "what happened", "actions taken", "log history"
                }
            };





        }
}