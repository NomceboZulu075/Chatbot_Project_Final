using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
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

            // Synonym dictionary for cybersecurity terms
            synonymDictionary = new Dictionary<string, List<string>>
            {
                ["password"] = new List<string> { "password", "passphrase", "login", "credentials", "pass" },
                ["two-factor"] = new List<string> { "2fa", "two-factor", "two factor", "mfa", "multi-factor" },
                ["phishing"] = new List<string> { "phishing", "scam", "fake email", "suspicious email", "fraud" },
                ["backup"] = new List<string> { "backup", "back up", "save", "copy", "archive" },
                ["antivirus"] = new List<string> { "antivirus", "anti-virus", "virus protection", "security software" },
                ["firewall"] = new List<string> { "firewall", "fire wall", "network protection", "security barrier" },
                ["update"] = new List<string> { "update", "upgrade", "patch", "install", "refresh" }
            };

            // Common typos dictionary and their corrections(fun feature!)
            commonTypos = new Dictionary<string, string>
            {
                ["taks"] = "task",
                ["psasword"] = "password",
                ["phising"] = "phishing",
                ["secruity"] = "security",
                ["updat"] = "update",
                ["quizz"] = "quiz",
                ["helpp"] = "help",
                ["shoow"] = "show"
            };

            // Conversational phrases for more natural interactions
            conversationalPhrases = new List<string>
            {
                "I understand you want to",
                "Let me help you with that!",
                "Great idea! I can assist with",
                "Perfect! Let's work on",
                "Absolutely! I'll help you",
                "Smart thinking! Let me"
            };

            AddToActivityLog("NLP System initialized with advanced understanding capabilities");
        }//end of InitializeNLPComponents method

        //A method to show a fun welcome message
        private void ShowWelcomeMessage()
        {
            AddChatbotResponse(" Welcome to the South African Awareness CyberSecurity Awareness Chatbot! 🛡️ ");
            AddChatbotResponse(" Here to help you stop phishing emails, craft uncrackable passwords and brown the web like a pro!");
            AddChatbotResponse(" Try saying things like:");
            AddChatbotResponse("   • 'Add task'");
            AddChatbotResponse("   • 'Show tasks'");
            AddChatbotResponse("   • 'Can you remind me to update my password?'");
            AddChatbotResponse("   • 'I need to enable 2FA tomorrow'");
            AddChatbotResponse("   • 'What have you been doing for me?'");
            AddChatbotResponse(" Type 'help' for commands or just ask me about cybersecurity! ✨");
        }//end of show welcome message

        // Initialize quiz questions
        private void InitializeQuizQuestions()
        {
            quizQuestions = new List<QuizQuestion>
            {

                new QuizQuestion
                {
                    Question = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> { "Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it" },
                    CorrectAnswer = 2,
                    Explanation = "✅ Correct! Reporting phishing emails helps prevent scams and protects others!",
                    IsTrueFalse = false
                },

                new QuizQuestion
                {
                    Question = "A strong password should be at least 12 characters long.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswer = 0,
                    Explanation = "✅ True! Security experts recommend passwords of at least 12 characters for better protection!",
                    IsTrueFalse = true
                },

                new QuizQuestion
                {
                    Question = "What is two-factor authentication (2FA)?",
                    Options = new List<string> { "Using two passwords", "An extra security step beyond password", "A type of antivirus", "A password manager" },
                    CorrectAnswer = 1,
                    Explanation = "✅ Exactly! 2FA adds an extra layer of security, like a text code or app notification!",
                    IsTrueFalse = false
                },

                new QuizQuestion
                {
                    Question = "It's safe to use public Wi-Fi for online banking.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswer = 1,
                    Explanation = "✅ False! Public Wi-Fi is not secure. Avoid sensitive activities like banking on public networks!",
                    IsTrueFalse = true
                },

                new QuizQuestion
                {
                    Question = "What is social engineering in cybersecurity?",
                    Options = new List<string> { "Building social media", "Manipulating people to reveal information", "Engineering software", "Creating social networks" },
                    CorrectAnswer = 1,
                    Explanation = "✅ Correct! Social engineering tricks people into giving away confidential information!",
                    IsTrueFalse = false
                },

                new QuizQuestion
                {
                    Question = "You should update your software regularly for security.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswer = 0,
                    Explanation = "✅ True! Software updates often include important security patches that protect against new threats!",
                    IsTrueFalse = true
                },

                new QuizQuestion
                {
                    Question = "What does 'HTTPS' indicate on a website?",
                    Options = new List<string> { "High Traffic Protocol", "Secure encrypted connection", "Hypertext Transfer", "Nothing important" },
                    CorrectAnswer = 1,
                    Explanation = "✅ Right! HTTPS means the connection is encrypted and more secure than HTTP!",
                    IsTrueFalse = false
                },

                new QuizQuestion
                {
                    Question = "It's okay to click on links in suspicious emails to see what they are.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswer = 1,
                    Explanation = "✅ False! Never click suspicious links - they could download malware or steal your information!",
                    IsTrueFalse = true
                },

                new QuizQuestion
                {
                    Question = "What should you do if your device gets infected with malware?",
                    Options = new List<string> { "Ignore it", "Disconnect from internet and scan", "Share your files quickly", "Keep using it normally" },
                    CorrectAnswer = 1,
                    Explanation = "✅ Correct! Disconnect immediately and run a full antivirus scan to prevent further damage!",
                    IsTrueFalse = false
                },
                new QuizQuestion
                {
                    Question = "Using the same password for multiple accounts is convenient and safe.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswer = 1,
                    Explanation = "✅ False! If one account gets hacked, all your accounts become vulnerable. Use unique passwords!",
                    IsTrueFalse = true
                }
            };
        }//end of initialize quiz questions method

        //A method to update the fun statistics display
        private void UpdateStatistics()
        {
            int completedTasks = cyberTasks.Count(i => i.IsCompleted);
            int totalTasks = cyberTasks.Count;
            int securityScore = totalTasks > 0 ? (completedTasks * 100) / totalTasks : 0;

            completed_count.Text = completedTasks.ToString();
            total_count.Text = totalTasks.ToString();
            security_score.Text = securityScore + "%";

            // Update quiz stats
            quiz_best_score.Text = bestScore + "%";
            quiz_count.Text = totalQuizzesTaken.ToString();

            // Update status based on score
            if (securityScore >= 80)
                status_text.Text = "🌟 Security Expert!";
            else if (securityScore >= 60)
                status_text.Text = "🔒 Getting Secure!";
            else if (securityScore >= 40)
                status_text.Text = "⚠️ Needs Work";
            else if (totalTasks > 0)
                status_text.Text = "🆘 Security Risk!";
            else
                status_text.Text = "I am ready to help!";

        }//end of update statistics method

        //A method to add entries to the activity log
        private void AddToActivityLog(string action)
        {
            string timestamp = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            activityLog.Add($"{timestamp}: {action}");

            // Keep only last 15 entries
            if (activityLog.Count > 15)
            {
                activityLog.RemoveAt(0);
            }//end of if statement
        }//end of add activity log method

        // ENHANCED METHOD: Add detailed activity log entries
        private void AddToEnhancedActivityLog(string action, string category, string details = "")
        {
            var logEntry = new ActivityLogEntry
            {
                Timestamp = DateTime.Now,
                Action = action,
                Category = category,
                Details = details
            };

            enhancedActivityLog.Add(logEntry);

            // Keep only last 50 entries for memory management
            if (enhancedActivityLog.Count > 50)
            {
                enhancedActivityLog.RemoveAt(0);
            }

            // Also add to the simple activity log for backward compatibility
            AddToActivityLog(action);
        }//end of add to enhanced activity log method

        // A method to handle showing detailed activity log
        private void HandleShowDetailedActivityLog()
        {
            if (enhancedActivityLog.Count == 0)
            {
                AddChatbotResponse(" No recent activities to show. Start using the chatbot to build your activity history!");
                return;
            }

            AddChatbotResponse(" Here's a summary of your recent cybersecurity activities:");

            // Show last 10 activities by default
            var recentActivities = enhancedActivityLog.TakeLast(MAX_LOG_DISPLAY).ToList();

            for (int i = 0; i < recentActivities.Count; i++)
            {
                var entry = recentActivities[i];
                string emoji = GetCategoryEmoji(entry.Category);
                string details = !string.IsNullOrEmpty(entry.Details) ? $" - {entry.Details}" : "";

                AddChatbotResponse($"{emoji} {i + 1}. {entry.Action}{details}");
            }

            // Show statistics
            AddChatbotResponse($"");
            AddChatbotResponse($" Activity Summary:");
            AddChatbotResponse($"   • Total actions logged: {enhancedActivityLog.Count}");
            AddChatbotResponse($"   • Tasks created: {enhancedActivityLog.Count(e => e.Category == "TASK_ADDED")}");
            AddChatbotResponse($"   • Quizzes taken: {enhancedActivityLog.Count(e => e.Category == "QUIZ_COMPLETED")}");
            AddChatbotResponse($"   • NLP interactions: {enhancedActivityLog.Count(e => e.Category == "NLP_INTERACTION")}");

            if (enhancedActivityLog.Count > MAX_LOG_DISPLAY)
            {
                AddChatbotResponse($"💡 Showing last {MAX_LOG_DISPLAY} activities. Type 'show full log' for complete history.");
            }
        }//end of handle show detailed activity log method

        // A method to show full activity log
        private void HandleShowFullActivityLog()
        {
            if (enhancedActivityLog.Count == 0)
            {
                AddChatbotResponse(" No activities recorded yet. 📝");
                return;
            }

            AddChatbotResponse($" Complete Activity History ({enhancedActivityLog.Count} total activities):");

            // Group activities by category for better organization
            var grouped = enhancedActivityLog.GroupBy(e => e.Category);

            foreach (var group in grouped)
            {
                string categoryName = GetCategoryDisplayName(group.Key);
                string emoji = GetCategoryEmoji(group.Key);
                AddChatbotResponse($"");
                AddChatbotResponse($"{emoji} {categoryName} ({group.Count()} activities):");

                foreach (var entry in group.TakeLast(5)) // Show last 5 per category
                {
                    string details = !string.IsNullOrEmpty(entry.Details) ? $" - {entry.Details}" : "";
                    AddChatbotResponse($"   • {entry.Timestamp:MM/dd HH:mm} - {entry.Action}{details}");
                }
            }
        }//end of handle full activity log method

        // A helper method to get emoji for activity category(fun feature!)
        private string GetCategoryEmoji(string category)
        {
            return category switch
            {
                "TASK_ADDED" => "✅",
                "TASK_COMPLETED" => "🎉",
                "TASK_UPDATED" => "🔄",
                "REMINDER_SET" => "⏰",
                "QUIZ_STARTED" => "🎯",
                "QUIZ_COMPLETED" => "🏆",
                "NLP_INTERACTION" => "🧠",
                "CYBERSECURITY_EDUCATION" => "🛡️",
                "SYSTEM" => "⚙️",
                _ => "📝"
            };
        }//end of get category emoji method

        // A helper method to get display name for category
        private string GetCategoryDisplayName(string category)
        {
            return category switch
            {
                "TASK_ADDED" => "Tasks Added",
                "TASK_COMPLETED" => "Tasks Completed",
                "TASK_UPDATED" => "Tasks Updated",
                "REMINDER_SET" => "Reminders Set",
                "QUIZ_STARTED" => "Quizzes Started",
                "QUIZ_COMPLETED" => "Quizzes Completed",
                "NLP_INTERACTION" => "Natural Language Processing",
                "CYBERSECURITY_EDUCATION" => "Cybersecurity Education",
                "SYSTEM" => "System Activities",
                _ => "Other Activities"
            };
        }//end of get category display name method

        // When the task is double clicked on the list view
        private void show_chats_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (show_chats.SelectedItem == null) return;

            string selectedItem = show_chats.SelectedItem.ToString();

            // Check if this is a task item (contains task information)
            if (selectedItem.Contains("[PENDING]") || selectedItem.Contains("[COMPLETED]"))
            {
                // Find the corresponding task
                var task = cyberTasks.FirstOrDefault(t => selectedItem.Equals(t.ToString()));

                if (task != null)
                {
                    // Toggle completion status
                    task.IsCompleted = !task.IsCompleted;
                    string action = task.IsCompleted ? "completed" : "marked as pending";

                    if (task.IsCompleted)
                    {
                        AddToEnhancedActivityLog($"Task completed: '{task.Title}'", "TASK_COMPLETED",
                            $"Created: {task.CreatedDate:MM/dd/yyyy}");
                    }
                    else
                    {
                        AddToEnhancedActivityLog($"Task reopened: '{task.Title}'", "TASK_UPDATED", "Marked as pending again");
                    }

                    // Refresh the display
                    RefreshTaskDisplay();

                    // Add chatbot response
                    string response = task.IsCompleted ?
                        $"Amazing work! Task '{task.Title}' has been marked as completed. Keep up the good cybersecurity practices!" :
                        $"Task '{task.Title}' has been marked as pending again. No worries, you've got this!";

                    AddChatbotResponse(response);
                }//end of inner if statement
            }//end of other outer if statement
        }//end of method showchatsMouseDoubleClick

        //A method to refresh task display in ListView
        private void RefreshTaskDisplay()
        {
            // Clear current display
            var chatItems = show_chats.Items.Cast<string>().Where(item =>
                !item.Contains("[PENDING]") && !item.Contains("[COMPLETED]")).ToList();

            show_chats.Items.Clear();

            // Add back non-task chat items
            foreach (var item in chatItems)
            {
                show_chats.Items.Add(item);
            }

            // Add all tasks
            foreach (var task in cyberTasks)
            {
                show_chats.Items.Add(task.ToString());
            }

            // Update stats after refreshing
            UpdateStatistics();

            // Auto scroll to bottom
            if (show_chats.Items.Count > 0)
            {
                show_chats.ScrollIntoView(show_chats.Items[show_chats.Items.Count - 1]);
            }
        }//end of RefreshTaskDisplay method

        //A method to add chatbot responses
        private void AddChatbotResponse(string response)
        {
            DateTime now = DateTime.Now;
            string timestamp = now.ToString("yyyy-MM-dd HH:mm");
            show_chats.Items.Add($"Chatbot [{timestamp}]: {response}");
            show_chats.ScrollIntoView(show_chats.Items[show_chats.Items.Count - 1]);
        }//end of AddChatbotResponse method

        // Method to add user messages
        private void AddUserMessage(string message)
        {
            DateTime now = DateTime.Now;
            string timestamp = now.ToString("yyyy-MM-dd HH:mm");
            show_chats.Items.Add($" 👤 User [{timestamp}]: {message}");
            show_chats.ScrollIntoView(show_chats.Items[show_chats.Items.Count - 1]);
        }//end of AddUserMessage method

        //A method for when the user asks a question or gives a command
        private void ask_question(object sender, RoutedEventArgs e)
        {
            string userInput = user_question.Text.Trim();

            if (string.IsNullOrEmpty(userInput))
            {
                MessageBox.Show("Please enter a question or command!");
                return;
            }

            // Add user message to chat
            AddUserMessage(userInput);

            // Clear input field
            user_question.Text = "";

            // Process the user input
            ProcessUserInput(userInput);
        }//end of ask_question method

        // Main method to process user input with advanced natural language understanding
        private void ProcessUserInput(string input)
        {
            string lowerInput = input.ToLower();

            // Check if we're in quiz mode
            if (isQuizActive)
            {
                HandleQuizAnswer(input);
                return;
            }

            // Use NLP to detect user intent
            string detectedIntent = DetectUserIntent(input);

            // Handle the detected intent with natural language understanding
            HandleNaturalLanguageResponse(detectedIntent, input);

            // Increment NLP interaction count for tracking
            nlpInteractionCount++;
        }//end of process user input method

        // A method to handle adding new tasks
        private void HandleAddTask(string input)
        {
            // Getting the title of a task from user input and generating a task description about that task
            string taskTitle = ExtractTaskTitle(input);
            string taskDescription = GenerateTaskDescription(taskTitle);

            // Create new task
            var newTask = new taskInformation
            {
                Title = taskTitle,
                Description = taskDescription,
                IsCompleted = false,
                CreatedDate = DateTime.Now
            };

            // Check if user wants a reminder
            DateTime? reminderDate = ExtractReminderDate(input);
            if (reminderDate.HasValue)
            {
                newTask.ReminderDate = reminderDate;
            }

            // Add task to the list
            string reminderDetails = reminderDate.HasValue ? $"with reminder on {reminderDate.Value:MM/dd/yyyy}" : "without reminder";
            AddToEnhancedActivityLog($"Task created: '{taskTitle}'", "TASK_ADDED", reminderDetails);

            if (reminderDate.HasValue)
            {
                AddToEnhancedActivityLog($"Reminder set for task '{taskTitle}' on {reminderDate.Value:MM/dd/yyyy}", "REMINDER_SET", taskTitle);
            }//end of if statement

            // Refreshing display and update statistics
            RefreshTaskDisplay();

            // Provide response with emojis
            string response = $"✅ Task added: '{taskTitle}' - {taskDescription}";
            if (reminderDate.HasValue)
            {
                response += $" ⏰ Reminder set for {reminderDate.Value:MM/dd/yyyy}.";
            }
            else
            {
                response += " Would you like to set a reminder for this task?";
            }

            AddChatbotResponse(response);

            // Fun encouragement based on task count
            if (cyberTasks.Count == 1)
                AddChatbotResponse(" Great start! Your first cybersecurity task is logged!");
            else if (cyberTasks.Count == 5)
                AddChatbotResponse(" You're on fire! 🔥 5 tasks and counting - security champion!");

        }//end of method handle add task

        // Extract task title from user input
        private string ExtractTaskTitle(string input)
        {
            // Simple extraction - look for common cybersecurity tasks
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("two-factor") || lowerInput.Contains("2fa"))
                return "Enable Two-Factor Authentication";
            else if (lowerInput.Contains("password"))
                return "Update Password";
            else if (lowerInput.Contains("privacy"))
                return "Review Privacy Settings";
            else if (lowerInput.Contains("backup"))
                return "Create Data Backup";
            else if (lowerInput.Contains("antivirus"))
                return "Update Antivirus Software";
            else if (lowerInput.Contains("firewall"))
                return "Check Firewall Settings";
            else if (lowerInput.Contains("software update"))
                return "Install Software Updates";

            else
            {
                // Try to extract from the input after "add task"
                string[] parts = input.Split(new string[] { "add task", "create task", "new task" },
                    StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                {
                    return parts[1].Trim();
                }
                return "General Cybersecurity Task";
            }//end of else statement
        }//end of extract task title method

        // Generate appropriate descriptions for each task
        private string GenerateTaskDescription(string title)
        {
            switch (title.ToLower())
            {
                case "enable two-factor authentication":
                    return "Set up 2FA on your important accounts to add an extra layer of security";
                case "update password":
                    return "Change your password to a strong, unique combination of special characteres, numbers and letters";
                case "review privacy settings":
                    return "Check and update privacy settings on your social media and online accounts";
                case "create data backup":
                    return "Backup important files to prevent data loss";
                case "update antivirus software":
                    return "Ensure your antivirus software is up to date at all times";
                case "check firewall settings":
                    return "Verify your firewall is properly configured";
                case "install software updates":
                    return "Install the latest security updates for your software";
                default:
                    return "Complete this cybersecurity task to improve your digital safety";
            }//end of switch case
        }//end of generate task description

        // Extract reminder date from user input
        private DateTime? ExtractReminderDate(string input)
        {
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("tomorrow"))
                return DateTime.Now.AddDays(1);
            else if (lowerInput.Contains("next week"))
                return DateTime.Now.AddDays(7);
            else if (lowerInput.Contains("3 days"))
                return DateTime.Now.AddDays(3);
            else if (lowerInput.Contains("5 days"))
                return DateTime.Now.AddDays(5);
            else if (lowerInput.Contains("1 week"))
                return DateTime.Now.AddDays(7);
            else if (lowerInput.Contains("2 weeks"))
                return DateTime.Now.AddDays(14);

            return null;
        }//end of extract reminder date method

        // A method to handle showing all the tasks
        private void HandleShowTasks()
        {
            if (cyberTasks.Count == 0)
            {
                AddChatbotResponse("You don't have any tasks yet. Would you like to add a cybersecurity task?");
                return;
            }

            string response = "Here are your cybersecurity tasks:\n";
            for (int i = 0; i < cyberTasks.Count; i++)
            {
                var task = cyberTasks[i];
                string status = task.IsCompleted ? "✅ Completed" : "⏳ Pending";
                string reminder = task.ReminderDate.HasValue ? $" (Reminder: {task.ReminderDate.Value:MM/dd/yyyy})" : "";
                string taskDisplay = $"{i + 1}. {status} {task.Title} - {task.Description}{reminder}";
                AddChatbotResponse(taskDisplay);
            }

            AddChatbotResponse(("💡 Tip: Double-click on any task above to mark it complete/incomplete!"));
        }//end of handle show tasks method

        // A method to handle deleting tasks
        private void HandleDeleteTask(string input)
        {
            if (cyberTasks.Count == 0)
            {
                AddChatbotResponse("No tasks available to delete!");
                return;
            }

            // Try to extract task number or title from input
            string lowerInput = input.ToLower();

            // Look for patterns like "delete task 1" or "remove task 2"
            var match = Regex.Match(lowerInput, @"(?:delete|remove)\s+task\s+(\d+)");
            if (match.Success)
            {
                int taskNumber = int.Parse(match.Groups[1].Value);
                if (taskNumber >= 1 && taskNumber <= cyberTasks.Count)
                {
                    var taskToDelete = cyberTasks[taskNumber - 1];
                    cyberTasks.RemoveAt(taskNumber - 1);
                    AddToActivityLog($"Task deleted: '{taskToDelete.Title}'");
                    RefreshTaskDisplay();
                    AddChatbotResponse($"✅ Task '{taskToDelete.Title}' has been deleted!");
                    return;
                }
            }

            // If no specific task number, show instructions
            AddChatbotResponse("To delete a task:");
            AddChatbotResponse("• Type 'delete task [number]' (e.g., 'delete task 1')");
            AddChatbotResponse("• Or double-click on a completed task in the list above to toggle it");
            HandleShowTasks(); // Show current tasks with numbers

        }//end of handle delete task method 

        // Handle showing activity log
        private void HandleShowActivityLog()
        {
            if (activityLog.Count == 0)
            {
                AddChatbotResponse("No recent activities to show.");
                return;
            }

            string response = "Here's your recent activity:\n";
            for (int i = Math.Max(0, activityLog.Count - 5); i < activityLog.Count; i++)
            {
                response += $"• {activityLog[i]}\n";
            }

            AddChatbotResponse(response);
        }//end of handle show activity log method

        // A method to handle help command, this guides the user 
        private void HandleHelp()
        {
            string helpText = " CyberBot Commands & Features:\n" +
                            "• 'Add task [description]' - Add a new cybersecurity task\n" +
                            "• 'Show tasks' - View all your tasks\n" +
                            "• 'Start quiz' - Test your cybersecurity knowledge\n" +
                            "• 'Show activity log' - View recent actions I've taken for you\n" +
                            "• 'Show full log' - View complete activity history\n" +
                            "• 'What have you done for me?' - Show summary of activities\n" +
                            "• 'Quiz statistics' - View your quiz performance\n" +
                            "• 'Activity log' - See recent actions\n" +
                            "• Ask me about phishing, passwords, malware, etc.\n" +
                            "• Double-click on tasks to mark them complete\n" +
                            "• Ask me about cybersecurity topics for advice!";

            AddChatbotResponse(helpText);
        }//end of handle help method


        // Quiz System Methods

        // Start the cybersecurity quiz
        private void StartQuiz()
        {
            if (quizQuestions.Count == 0)
            {
                AddChatbotResponse("❌ Sorry, no quiz questions available!");
                return;
            }

            isQuizActive = true;
            currentQuestionIndex = 0;
            quizScore = 0;
            totalQuizzesTaken++;

            // Shuffle questions for variety
            var random = new Random();
            quizQuestions = quizQuestions.OrderBy(x => random.Next()).ToList();

            AddToActivityLog("Quiz started");
            AddChatbotResponse(" Welcome to the Cybersecurity Knowledge Quiz! ");
            AddChatbotResponse("📝 Answer questions by typing the number (1, 2, 3, 4) or 'A', 'B', 'C', 'D'");
            AddChatbotResponse("⚡ For True/False questions, type '1' for True or '2' for False");
            AddChatbotResponse(" Let's begin! Good luck! 🍀");

            ShowCurrentQuestion();
        }//end of start quiz method

        // Display current quiz question
        private void ShowCurrentQuestion()
        {
            if (currentQuestionIndex >= quizQuestions.Count)
            {
                EndQuiz();
                return;
            }//end of if statement

            var question = quizQuestions[currentQuestionIndex];
            string questionText = $"❓ Question {currentQuestionIndex + 1}/{quizQuestions.Count}:\n{question.Question}";

            AddChatbotResponse(questionText);

            // Show options
            if (question.IsTrueFalse)
            {
                AddChatbotResponse("1️⃣ True\n2️⃣ False");
            }
            else
            {
                for (int i = 0; i < question.Options.Count; i++)
                {
                    string emoji = new string[] { "1️⃣", "2️⃣", "3️⃣", "4️⃣" }[i];
                    AddChatbotResponse($"{emoji} {question.Options[i]}");
                }
            }
        }//end of show current question method

        // Handle quiz answers
        private void HandleQuizAnswer(string input)
        {
            // Check if user wants to quit the quiz
            string lowerInput = input.ToLower().Trim();
            if (lowerInput.Contains("quit quiz") || lowerInput.Contains("stop quiz") ||
                lowerInput.Contains("end quiz") || lowerInput.Contains("exit quiz"))
            {
                isQuizActive = false;
                AddChatbotResponse(" Quiz ended! You can start a new quiz anytime by typing 'start quiz'.");
                AddToActivityLog("Quiz ended by user request");
                return;
            }

            var question = quizQuestions[currentQuestionIndex];
            int userAnswer = -1;

            // Parse user input
            string cleanInput = input.Trim().ToLower();

            if (cleanInput == "1" || cleanInput == "a")
                userAnswer = 0;
            else if (cleanInput == "2" || cleanInput == "b")
                userAnswer = 1;
            else if (cleanInput == "3" || cleanInput == "c")
                userAnswer = 2;
            else if (cleanInput == "4" || cleanInput == "d")
                userAnswer = 3;
            else
            {
                AddChatbotResponse(" Please answer with 1, 2, 3, 4 or A, B, C, D");
                return;
            }

            // using an if statement to validate answer range
            if (userAnswer >= question.Options.Count)
            {
                AddChatbotResponse($"❌ Invalid option! Please choose 1-{question.Options.Count}");
                return;
            }

            // Checking if the answer is correct
            bool isCorrect = userAnswer == question.CorrectAnswer;
            if (isCorrect)
            {
                quizScore++;
                AddChatbotResponse($"✅ {question.Explanation}");

                // Fun encouragement messages
                string[] encouragements = { "Amazing! 🌟", "You're on fire! 🔥", "Brilliant! 💡", "Fantastic! 🎉", "Perfect! 👌" };
                var random = new Random();
                AddChatbotResponse(encouragements[random.Next(encouragements.Length)]);
            }
            else
            {
                AddChatbotResponse($"❌ Incorrect. The right answer was: {question.Options[question.CorrectAnswer]}");
                AddChatbotResponse(question.Explanation.Replace(" Correct!", "💡 Remember:"));
            }

            currentQuestionIndex++;

            // Small delay before next question
            AddChatbotResponse("─────────────────────");
            ShowCurrentQuestion();
        }//end of handle quiz answer method

        // End the quiz and show results
        private void EndQuiz()
        {
            isQuizActive = false;
            int percentage = (quizScore * 100) / quizQuestions.Count;

            // Update best score
            if (percentage > bestScore)
            {
                bestScore = percentage;
                AddChatbotResponse("🏆 NEW PERSONAL BEST! 🏆");
            }

            AddToEnhancedActivityLog($"Quiz completed: {quizScore}/{quizQuestions.Count} correct ({percentage}%)",
            "QUIZ_COMPLETED", $"Best score: {bestScore}%");

            AddChatbotResponse(" Quiz Complete! ");
            AddChatbotResponse($"📊 Your Score: {quizScore}/{quizQuestions.Count} ({percentage}%)");

            // Fun feedback based on score the user has received
            if (percentage >= 90)
            {
                AddChatbotResponse(" OUTSTANDING! 🌟 You're a cybersecurity expert!");
                AddChatbotResponse("You could teach others about online safety! 🛡");
            }
            else if (percentage >= 80)
            {
                AddChatbotResponse(" EXCELLENT! 🌟 You have strong cybersecurity knowledge! 💪");
                AddChatbotResponse(" Keep up the great security habits! 🛡");
            }
            else if (percentage >= 70)
            {
                AddChatbotResponse(" GOOD JOB! 👍 You know the basics well! ");
                AddChatbotResponse(" A little more practice and you'll be an expert! 🛡️");
            }
            else if (percentage >= 60)
            {
                AddChatbotResponse(" FAIR! You're on the right track! ");
                AddChatbotResponse(" Consider reviewing cybersecurity best practices! 🛡️");
            }
            else
            {
                AddChatbotResponse(" KEEP LEARNING! Cybersecurity is important! ⚠️");
                AddChatbotResponse(" Try reading about phishing, passwords, and safe browsing! 🛡️");
            }

            AddChatbotResponse(" Type 'start quiz' to try again or 'quiz stats' to see your progress!");

            AddToActivityLog($"Quiz completed - Score: {quizScore}/{quizQuestions.Count} ({percentage}%)");
        }//end of end quiz method

        // Show quiz statistics
        private void ShowQuizStatistics()
        {
            if (totalQuizzesTaken == 0)
            {
                AddChatbotResponse(" No quizzes taken yet! Type 'start quiz' to begin! ");
                return;
            }

            string stats = $"🏆 Your Quiz Statistics:\n";
            stats += $"📝 Total Quizzes Taken: {totalQuizzesTaken}\n";
            stats += $"🥇 Best Score: {bestScore}%\n";

            // Adding achievement badges - fun feature
            if (bestScore >= 90)
                stats += "🏅 Achievement: Cybersecurity Expert!\n";
            else if (bestScore >= 80)
                stats += "🥈 Achievement: Security Specialist!\n";
            else if (bestScore >= 70)
                stats += "🥉 Achievement: Safety Student!\n";

            if (totalQuizzesTaken >= 5)
                stats += " Achievement: Quiz Master!\n";

            stats += "\n Keep practicing to improve your cybersecurity knowledge! 💡";

            AddChatbotResponse(stats);
        }//end of mehod show quiz statistics

        // A method to handle quitting quiz

        private void HandleQuitQuiz()
        {
            if (!isQuizActive)
            {
                AddChatbotResponse("No quiz is currently active.");
                return;
            }

            isQuizActive = false;
            currentQuestionIndex = 0;
            quizScore = 0;

            AddChatbotResponse("❌ Quiz cancelled. Type 'start quiz' when you're ready to try again!");
            AddToActivityLog("Quiz cancelled by user");
        }//end of handle quit quiz 

        // A method to handle general cybersecurity queries
        private void HandleGeneralQuery(string input)
        {
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("phishing"))
            {
                AddChatbotResponse("Phishing is a cyber attack where criminals impersonate legitimate organizations to steal sensitive information. Always verify sender identity and never click suspicious links!");
            }
            else if (lowerInput.Contains("strong password"))
            {
                AddChatbotResponse("A strong password should be at least 12 characters long, include uppercase and lowercase letters, numbers, and symbols. Never reuse passwords across accounts!");
            }
            else if (lowerInput.Contains("malware"))
            {
                AddChatbotResponse("Malware is malicious software designed to harm your computer. Keep your antivirus updated, avoid suspicious downloads, and regularly scan your system!");
            }
            else if (lowerInput.Contains("social engineering"))
            {
                AddChatbotResponse("Social engineering involves manipulating people to divulge confidential information. Be cautious of unsolicited requests for personal information, even if they seem legitimate!");
            }
            else
            {
                AddChatbotResponse("I'm here to help with cybersecurity awareness! You can ask me about phishing, passwords, malware, or use commands like 'add task' or 'show tasks'. Type 'help' for more commands.");
            }

            AddToActivityLog($"Responded to query about: {input}");
        }//end of handle general query method







    }
}