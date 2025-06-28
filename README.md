# 🔒 Cybersecurity Awareness Chatbot (WPF/C#)

A GUI-based chatbot that educates users about cybersecurity through task management, interactive quizzes, and natural language processing (NLP) simulations.

![Screenshot 2025-06-28 020715](https://github.com/user-attachments/assets/55083132-24bb-441e-87b5-86b28d326164)

)

## ✨ Features

### 1. Task Assistant
- Add, delete, and mark cybersecurity tasks as complete (e.g., "Enable 2FA")
- Set optional reminders (e.g., "Remind me in 3 days")
- Progress tracking with completion statistics

### 2. Cybersecurity Quiz
- 10+ questions (mix of MCQ/True-False) on topics like:
  - Phishing
  - Password security
  - Social engineering
- Immediate feedback with explanations
- Score tracking and performance analytics

  ### 3. NLP Simulation
- Understands varied user phrasing:
  - "Add task to update password" → Creates task
  - "Remind me about 2FA tomorrow" → Sets reminder
- Keyword and synonym detection (e.g., "2FA" = "two-factor authentication")

### 4. Activity Log
- Tracks all user interactions (tasks, quizzes, NLP commands)
- Viewable via "Show activity log" command
- Timestamps and categorization

## 🛠️ Tech Stack
- **Language**: C#
- **Framework**: WPF (.NET 6+)
- **UI**: XAML with Material Design styling
- **NLP**: Regex/string manipulation (simulated)

## 📥 Installation
1. **Prerequisites**:
   - Visual Studio 2022 (or VSCode with C# extension)
  
  2. **Run Locally**:
   ```bash
   git clone https://github.com/NomceboZulu075/Chatbot_Project_Final.git
   cd cybersecurity-chatbot/Chatbot_Project_FinalPart3
   dotnet run
