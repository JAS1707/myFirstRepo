using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new GameForm());
    }
}

public class GameForm : Form
{
    private readonly List<Driver> drivers = new()
    {
        new Driver(
            "Max Verstappen",
            new[]
            {
                "Red Bull driver and reigning world champion.",
                "Dutch driver who has dominated recent seasons.",
                "Known for his aggressive driving style and multiple titles."
            },
            "Red Bull",
            "Netherlands",
            "Races with car number 1."),
        new Driver(
            "Sergio Pérez",
            new[]
            {
                "Red Bull teammate of Max Verstappen.",
                "Mexican driver with strong race pace.",
                "Often called Checo and has podium finishes."
            },
            "Red Bull",
            "Mexico",
            "Joined Red Bull in 2021."),
        new Driver(
            "Lewis Hamilton",
            new[]
            {
                "Seven-time world champion.",
                "Races for Ferrari in the 2026 season.",
                "Holds numerous F1 records including pole positions."
            },
            "Ferrari",
            "United Kingdom",
            "Drives with number 44."),
        new Driver(
            "George Russell",
            new[]
            {
                "Mercedes driver and teammate of Lewis Hamilton.",
                "British driver who moved from Williams.",
                "Known for strong qualifying and race performances."
            },
            "Mercedes",
            "United Kingdom",
            "Joined Mercedes in 2022."),
        new Driver(
            "Charles Leclerc",
            new[]
            {
                "Ferrari driver from Monaco.",
                "Known for his qualifying speed.",
                "Aiming for his first world title in 2026."
            },
            "Ferrari",
            "Monaco",
            "Often challenges for pole position."),
        new Driver(
            "Carlos Sainz",
            new[]
            {
                "Spanish Ferrari driver.",
                "Teammate of Charles Leclerc.",
                "Known for consistent and strategic driving."
            },
            "Ferrari",
            "Spain",
            "Drove for McLaren before Ferrari."),
        new Driver(
            "Lando Norris",
            new[]
            {
                "British McLaren driver.",
                "Popular on social media.",
                "Has shown strong pace in recent seasons."
            },
            "McLaren",
            "United Kingdom",
            "Races with car number 4."),
        new Driver(
            "Oscar Piastri",
            new[]
            {
                "Australian McLaren driver.",
                "Teammate of Lando Norris.",
                "Former Formula 2 champion."
            },
            "McLaren",
            "Australia",
            "Rookie in 2023, now established."),
        new Driver(
            "Fernando Alonso",
            new[]
            {
                "Veteran Spanish driver with two world titles.",
                "Races for Aston Martin.",
                "Returned to F1 and remains competitive."
            },
            "Aston Martin",
            "Spain",
            "World champion in 2005 and 2006."),
        new Driver(
            "Lance Stroll",
            new[]
            {
                "Canadian Aston Martin driver.",
                "Teammate of Fernando Alonso.",
                "Son of Lawrence Stroll, team owner."
            },
            "Aston Martin",
            "Canada",
            "Has raced since 2017."),
        new Driver(
            "Pierre Gasly",
            new[]
            {
                "French Alpine driver.",
                "Former AlphaTauri winner.",
                "Known for strong overtaking moves."
            },
            "Alpine",
            "France",
            "Races with Alpine since 2023."),
        new Driver(
            "Esteban Ocon",
            new[]
            {
                "French Alpine driver.",
                "Teammate of Pierre Gasly.",
                "Won his first GP at Hungary."
            },
            "Alpine",
            "France",
            "Has been with Alpine since 2020."),
        new Driver(
            "Alexander Albon",
            new[]
            {
                "Thai Williams driver.",
                "Former Red Bull junior.",
                "Known for smooth driving style."
            },
            "Williams",
            "Thailand",
            "Raced for Toro Rosso and Red Bull."),
        new Driver(
            "Franco Colapinto",
            new[]
            {
                "Argentinian Williams driver.",
                "Young talent in F1.",
                "Made his debut in 2024."
            },
            "Williams",
            "Argentina",
            "Rookie driver for 2026."),
        new Driver(
            "Nico Hülkenberg",
            new[]
            {
                "German Kick Sauber driver.",
                "Experienced driver with multiple teams.",
                "Known as a solid performer."
            },
            "Kick Sauber",
            "Germany",
            "Has raced for Force India, Renault."),
        new Driver(
            "Valtteri Bottas",
            new[]
            {
                "Finnish Kick Sauber driver.",
                "Former Mercedes driver.",
                "Held pole positions and podiums."
            },
            "Kick Sauber",
            "Finland",
            "Raced for Williams and Alfa Romeo."),
        new Driver(
            "Kevin Magnussen",
            new[]
            {
                "Danish Haas driver.",
                "Teammate of Oliver Bearman.",
                "Known for his aggressive style."
            },
            "Haas",
            "Denmark",
            "Raced for McLaren and Renault."),
        new Driver(
            "Oliver Bearman",
            new[]
            {
                "British Haas driver.",
                "Young driver with Ferrari academy background.",
                "Made F1 appearances as reserve."
            },
            "Haas",
            "United Kingdom",
            "Rookie for Haas in 2026."),
        new Driver(
            "Yuki Tsunoda",
            new[]
            {
                "Japanese RB driver.",
                "Known for his energetic driving.",
                "Former AlphaTauri driver."
            },
            "RB",
            "Japan",
            "Raced for AlphaTauri since 2021."),
        new Driver(
            "Daniel Ricciardo",
            new[]
            {
                "Australian RB driver.",
                "Former McLaren driver.",
                "Known for his smile and race wins."
            },
            "RB",
            "Australia",
            "Returned to F1 with RB in 2024.")
    };

    private readonly Random random = new();
    private Driver? currentDriver;
    private Difficulty difficulty = Difficulty.Medium;
    private int attemptsLeft;
    private int clueIndex;
    private int score;
    private bool paused;

    private Label labelTitle = null!;
    private Label labelInstructions = null!;
    private Label labelClue = null!;
    private Label labelAttempts = null!;
    private Label labelScore = null!;
    private Label labelFeedback = null!;
    private TextBox textBoxGuess = null!;
    private Button buttonGuess = null!;
    private Button buttonNewRound = null!;
    private Button buttonStart = null!;
    private ComboBox comboBoxDifficulty = null!;
    private Panel pausePanel = null!;
    private Button buttonResume = null!;
    private Button buttonQuit = null!;

    public GameForm()
    {
        InitializeComponent();
        BuildInterface();
        ShowStartScreen();
    }

    private void InitializeComponent()
    {
        SuspendLayout();
        Text = "2026 F1 Driver Guessing Game";
        ClientSize = new Size(760, 520);
        MinimumSize = new Size(760, 520);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(245, 245, 245);
        Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        KeyPreview = true;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        KeyDown += GameForm_KeyDown;
        ResumeLayout(false);
    }

    private void BuildInterface()
    {
        labelTitle = new Label
        {
            Text = "2026 F1 Driver Guessing Game",
            Font = new Font(Font.FontFamily, 20F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(24, 20)
        };

        labelInstructions = new Label
        {
            Text = "Select a difficulty and press Start. Press ESC anytime to pause.",
            AutoSize = true,
            Location = new Point(26, 68),
            MaximumSize = new Size(700, 0)
        };

        comboBoxDifficulty = new ComboBox
        {
            Location = new Point(26, 120),
            Size = new Size(200, 28),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        comboBoxDifficulty.Items.AddRange(new object[] { Difficulty.Easy, Difficulty.Medium, Difficulty.Hard, Difficulty.Endless });
        comboBoxDifficulty.SelectedIndex = 1;

        buttonStart = new Button
        {
            Text = "Start Game",
            Location = new Point(250, 118),
            Size = new Size(130, 30)
        };
        buttonStart.Click += ButtonStart_Click;

        labelClue = new Label
        {
            Text = "Clue:",
            Font = new Font(Font.FontFamily, 12F, FontStyle.Regular, GraphicsUnit.Point),
            Location = new Point(26, 180),
            Size = new Size(700, 80)
        };

        textBoxGuess = new TextBox
        {
            Location = new Point(26, 280),
            Size = new Size(470, 30)
        };
        textBoxGuess.KeyDown += TextBoxGuess_KeyDown;

        buttonGuess = new Button
        {
            Text = "Guess",
            Location = new Point(510, 278),
            Size = new Size(100, 30)
        };
        buttonGuess.Click += ButtonGuess_Click;

        buttonNewRound = new Button
        {
            Text = "Next Round",
            Location = new Point(626, 278),
            Size = new Size(100, 30),
            Visible = false
        };
        buttonNewRound.Click += ButtonNewRound_Click;

        labelAttempts = new Label
        {
            Text = "Attempts: 0",
            Location = new Point(26, 330),
            Size = new Size(220, 24)
        };

        labelScore = new Label
        {
            Text = "Score: 0",
            Location = new Point(260, 330),
            Size = new Size(220, 24)
        };

        labelFeedback = new Label
        {
            Text = string.Empty,
            Location = new Point(26, 370),
            Size = new Size(700, 80),
            ForeColor = Color.DarkBlue,
            AutoSize = false
        };

        pausePanel = new Panel
        {
            Size = new Size(ClientSize.Width, ClientSize.Height),
            Location = new Point(0, 0),
            BackColor = Color.FromArgb(220, Color.Black),
            Visible = false
        };

        var pauseLabel = new Label
        {
            Text = "Game Paused",
            ForeColor = Color.White,
            Font = new Font(Font.FontFamily, 18F, FontStyle.Bold, GraphicsUnit.Point),
            AutoSize = true,
            Location = new Point(320, 140)
        };

        buttonResume = new Button
        {
            Text = "Resume",
            Size = new Size(120, 40),
            Location = new Point(320, 220)
        };
        buttonResume.Click += ButtonResume_Click;

        buttonQuit = new Button
        {
            Text = "Quit Game",
            Size = new Size(120, 40),
            Location = new Point(320, 280)
        };
        buttonQuit.Click += ButtonQuit_Click;

        pausePanel.Controls.Add(pauseLabel);
        pausePanel.Controls.Add(buttonResume);
        pausePanel.Controls.Add(buttonQuit);

        Controls.Add(labelTitle);
        Controls.Add(labelInstructions);
        Controls.Add(comboBoxDifficulty);
        Controls.Add(buttonStart);
        Controls.Add(labelClue);
        Controls.Add(textBoxGuess);
        Controls.Add(buttonGuess);
        Controls.Add(buttonNewRound);
        Controls.Add(labelAttempts);
        Controls.Add(labelScore);
        Controls.Add(labelFeedback);
        Controls.Add(pausePanel);
    }

    private void ShowStartScreen()
    {
        labelClue.Text = "Choose a difficulty level and press Start to begin the game.";
        textBoxGuess.Enabled = false;
        buttonGuess.Enabled = false;
        buttonNewRound.Visible = false;
        labelAttempts.Text = string.Empty;
        labelScore.Text = "Score: 0";
        labelFeedback.Text = string.Empty;
    }

    private void ButtonStart_Click(object? sender, EventArgs e)
    {
        difficulty = (Difficulty)comboBoxDifficulty.SelectedItem!;
        score = 0;
        UpdateScoreDisplay();
        StartNewRound();
    }

    private void StartNewRound()
    {
        currentDriver = drivers[random.Next(drivers.Count)];
        attemptsLeft = difficulty switch
        {
            Difficulty.Hard => 2,
            Difficulty.Medium => 3,
            Difficulty.Easy => 4,
            Difficulty.Endless => 1,
            _ => 3
        };
        clueIndex = 0;
        labelClue.Text = $"Clue 1: {currentDriver.Clues[0]}";
        labelFeedback.Text = string.Empty;
        textBoxGuess.Text = string.Empty;
        textBoxGuess.Enabled = true;
        buttonGuess.Enabled = true;
        buttonNewRound.Visible = false;
        UpdateAttemptDisplay();
        UpdateScoreDisplay();
        textBoxGuess.Focus();
    }

    private void ButtonGuess_Click(object? sender, EventArgs e)
    {
        SubmitGuess();
    }

    private void TextBoxGuess_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            SubmitGuess();
        }
    }

    private void SubmitGuess()
    {
        if (currentDriver is null)
        {
            return;
        }

        var guess = textBoxGuess.Text.Trim();
        if (string.IsNullOrEmpty(guess))
        {
            labelFeedback.ForeColor = Color.DarkRed;
            labelFeedback.Text = "Enter a driver name before guessing.";
            return;
        }

        if (string.Equals(guess, currentDriver.Name, StringComparison.OrdinalIgnoreCase))
        {
            var points = attemptsLeft * 10;
            score += points;
            labelFeedback.ForeColor = Color.Green;
            labelFeedback.Text = $"Correct! {currentDriver.Name} is the driver.\nTeam: {currentDriver.Team} | Nationality: {currentDriver.Nationality} | {currentDriver.ExtraInfo}\nYou earned {points} points.";
            UpdateScoreDisplay();
            if (difficulty == Difficulty.Endless)
            {
                StartNewRound();
            }
            else
            {
                EndRound();
            }
            return;
        }

        attemptsLeft--;
        if (attemptsLeft > 0)
        {
            labelFeedback.ForeColor = Color.DarkOrange;
            labelFeedback.Text = "Wrong guess. Here is another clue.";
            clueIndex = Math.Min(clueIndex + 1, currentDriver.Clues.Length - 1);
            labelClue.Text = $"Clue {clueIndex + 1}: {currentDriver.Clues[clueIndex]}";
            UpdateAttemptDisplay();
        }
        else
        {
            labelFeedback.ForeColor = Color.DarkRed;
            if (difficulty == Difficulty.Endless)
            {
                labelFeedback.Text = $"Wrong guess. Game Over!\nThe correct answer was {currentDriver.Name}.\nTeam: {currentDriver.Team} | Nationality: {currentDriver.Nationality}\nFinal Score: {score}";
                buttonGuess.Enabled = false;
                textBoxGuess.Enabled = false;
                buttonNewRound.Visible = true;
                buttonNewRound.Text = "Play Again";
                buttonNewRound.Enabled = true;
            }
            else
            {
                labelFeedback.Text = $"Out of guesses. The correct answer was {currentDriver.Name}.\nTeam: {currentDriver.Team} | Nationality: {currentDriver.Nationality}";
                attemptsLeft = 0;
                UpdateAttemptDisplay();
                EndRound();
            }
        }
    }

    private void EndRound()
    {
        buttonGuess.Enabled = false;
        textBoxGuess.Enabled = false;
        buttonNewRound.Visible = true;
        buttonNewRound.Enabled = true;
    }

    private void ButtonNewRound_Click(object? sender, EventArgs e)
    {
        if (difficulty == Difficulty.Endless && buttonNewRound.Text == "Play Again")
        {
            score = 0;
            UpdateScoreDisplay();
            buttonNewRound.Text = "Next Round";
        }
        StartNewRound();
    }

    private void GameForm_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        paused = !paused;
        pausePanel.Visible = paused;
        textBoxGuess.Enabled = !paused && currentDriver is not null && attemptsLeft > 0;
        buttonGuess.Enabled = !paused && currentDriver is not null && attemptsLeft > 0;
        buttonNewRound.Enabled = !paused && buttonNewRound.Visible;
        buttonStart.Enabled = !paused;
        comboBoxDifficulty.Enabled = !paused;
    }

    private void ButtonResume_Click(object? sender, EventArgs e)
    {
        TogglePause();
    }

    private void ButtonQuit_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void UpdateAttemptDisplay()
    {
        if (difficulty == Difficulty.Endless)
        {
            labelAttempts.Text = "Endless: one wrong guess ends the game";
        }
        else
        {
            labelAttempts.Text = $"Attempts left: {attemptsLeft}";
        }
    }

    private void UpdateScoreDisplay()
    {
        labelScore.Text = $"Score: {score}";
    }
}

public record Driver(string Name, string[] Clues, string Team, string Nationality, string ExtraInfo);

public enum Difficulty
{
    Easy,
    Medium,
    Hard,
    Endless
}
