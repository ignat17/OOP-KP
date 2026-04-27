using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using DistLearn;
using DistLearn.WPF.Data;
using Microsoft.Win32;

namespace DistLearn.WPF
{
    public partial class AssignmentWindow : Window
    {
        private Assignment assignment;
        private Student currentStudent;
        private Teacher currentTeacher;
        private Submission currentSubmission;

        public AssignmentWindow(Assignment selectedAssignment)
        {
            InitializeComponent();

            assignment = selectedAssignment;
            currentStudent = AppData.CurrentUser as Student;
            currentTeacher = AppData.CurrentUser as Teacher;
            currentSubmission = null;

            LoadAssignment();
        }

        private void LoadAssignment()
        {
            if (assignment == null)
            {
                MessageBox.Show("Завдання не знайдено.");
                Close();
                return;
            }

            ContentGrid.ColumnDefinitions[0].Width = new GridLength(1.1, GridUnitType.Star);
            ContentGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
            Grid.SetColumnSpan(InfoBorder, 1);
            InfoBorder.Margin = new Thickness(0, 0, 12, 0);

            TitleText.Text = assignment.Title;
            DescText.Text = assignment.Description;
            InstructionsText.Text = assignment.Instructions;
            ScoreText.Text = assignment.MaxScore.ToString();
            DeadlineText.Text = assignment.Deadline.ToShortDateString();

            if (currentStudent != null)
            {
                StudentBlock.Visibility = Visibility.Visible;
                TeacherBlock.Visibility = Visibility.Collapsed;
                LoadStudentSubmission();
            }
            else if (currentTeacher != null)
            {
                StudentBlock.Visibility = Visibility.Collapsed;
                TeacherBlock.Visibility = Visibility.Visible;
                LoadTeacherSubmissions();
            }
            else
            {
                StudentBlock.Visibility = Visibility.Collapsed;
                TeacherBlock.Visibility = Visibility.Collapsed;

                ContentGrid.ColumnDefinitions[1].Width = new GridLength(0);
                Grid.SetColumnSpan(InfoBorder, 2);
                InfoBorder.Margin = new Thickness(0);

                Width = 850;
                Height = 500;
            }
        }

        private void LoadStudentSubmission()
        {
            StatusText.Text = "Ще не здано";
            FilePathBox.Text = "";
            CommentBox.Text = "";

            for (int i = 0; i < AppData.Submissions.Count; i++)
            {
                Submission submission = AppData.Submissions[i];

                if (submission.Student != null &&
                    submission.Assignment != null &&
                    submission.Student.Login == currentStudent.Login &&
                    submission.Assignment.Title == assignment.Title)
                {
                    currentSubmission = submission;
                    FilePathBox.Text = submission.FilePath;
                    CommentBox.Text = submission.Comment;
                    StatusText.Text = submission.Status;
                    break;
                }
            }
        }

        private void LoadTeacherSubmissions()
        {
            SubmissionsList.Items.Clear();
            SubmissionCommentText.Text = "";
            SubmissionFileText.Text = "";

            for (int i = 0; i < AppData.Submissions.Count; i++)
            {
                Submission submission = AppData.Submissions[i];

                if (submission.Assignment != null &&
                    submission.Assignment.Title == assignment.Title &&
                    submission.Student != null)
                {
                    SubmissionsList.Items.Add(
                        submission.Student.Login + " - " + submission.Status);
                }
            }

            if (SubmissionsList.Items.Count == 0)
            {
                SubmissionsList.Items.Add("Роботи відсутні");
            }
        }

        private void ChooseFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                FilePathBox.Text = dialog.FileName;
            }
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentStudent == null)
            {
                return;
            }

            if (DateTime.Now > assignment.Deadline)
            {
                MessageBox.Show("Дедлайн уже минув.");
                return;
            }

            string filePath = FilePathBox.Text;
            string comment = CommentBox.Text;

            if (filePath == "" && comment == "")
            {
                MessageBox.Show("Виберіть файл або введіть коментар.");
                return;
            }

            if (currentSubmission == null)
            {
                currentSubmission = currentStudent.SubmitAssignment(assignment, filePath, comment);

                if (currentSubmission == null)
                {
                    MessageBox.Show("Не вдалося створити відповідь.");
                    return;
                }

                currentSubmission.Send();
                AppData.Submissions.Add(currentSubmission);
            }
            else
            {
                currentSubmission.FilePath = filePath;
                currentSubmission.Comment = comment;
                currentSubmission.Send();
            }

            StatusText.Text = currentSubmission.Status;
            MessageBox.Show("Роботу успішно здано.");
        }

        private void SubmissionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedText = SubmissionsList.SelectedItem as string;

            if (selectedText == null || selectedText == "Роботи відсутні")
            {
                return;
            }

            string login = selectedText.Split(' ')[0];

            for (int i = 0; i < AppData.Submissions.Count; i++)
            {
                Submission submission = AppData.Submissions[i];

                if (submission.Student != null &&
                    submission.Assignment != null &&
                    submission.Student.Login == login &&
                    submission.Assignment.Title == assignment.Title)
                {
                    currentSubmission = submission;
                    SubmissionCommentText.Text = submission.Comment;
                    SubmissionFileText.Text = submission.FilePath;
                    break;
                }
            }
        }

        private void OpenFileBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentSubmission == null)
            {
                MessageBox.Show("Оберіть роботу.");
                return;
            }

            if (currentSubmission.FilePath == null || currentSubmission.FilePath == "")
            {
                MessageBox.Show("Файл не прикріплено.");
                return;
            }

            if (!File.Exists(currentSubmission.FilePath))
            {
                MessageBox.Show("Файл не знайдено.");
                return;
            }

            Process.Start(new ProcessStartInfo(currentSubmission.FilePath)
            {
                UseShellExecute = true
            });
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}