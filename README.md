# Astro Kitchen

**CS-465 Final Project — Spring 2026, Colorado State University**

A virtual reality project comparing controller ray-casting and head-gaze selection in a space-themed cooking task. Built in Unity for the Meta Quest 2.

## Group Members

- Ziqiu Feng — Ziqiu.Feng@colostate.edu
- Kyle Wong — Kyle.Wong@colostate.edu
- Zachary Kinnaman — zack722@colostate.edu

## Project Overview

Astro Kitchen is a small VR prototype loosely inspired by *Overcooked*. Players complete short cooking recipes by selecting ingredients from shelves and dropping them into a cooking pot in a zero-gravity space kitchen. We ran a within-subjects qualitative study with six VR-novice participants and used thematic analysis to compare the two selection techniques. All six participants preferred ray-casting over head-gaze.

The full paper is in this submission as `Astro_Kitchen_Final_Report.pdf`.

---

## Final Videos

### Project Overview Video
- View link: https://drive.google.com/file/d/1Jdaj1kx1a6YQF9d5qS89xLxY-cqTzxec/view?usp=drive_link
- Download link: https://drive.google.com/file/d/1Jdaj1kx1a6YQF9d5qS89xLxY-cqTzxec/view?usp=drive_link

### Project Code Video
- View link: https://drive.google.com/file/d/1kbJO-KxmFe48qZJPz73ppXYIjeviGwpJ/view?usp=drive_link
- Download link: https://drive.google.com/file/d/1kbJO-KxmFe48qZJPz73ppXYIjeviGwpJ/view?usp=drive_link

### Project Presentation Video
- View link: [TO BE ADDED before submission]
- Download link: [TO BE ADDED before submission]

---

## Repository / Code Access

### GitHub
https://github.com/csu-hci-projects/SP26-Astro-Kitchen/tree/main

### Unity Project (Google Drive)
The full Unity project is also hosted on Google Drive in case the GitHub repo doesn't include large asset binaries:

https://drive.google.com/drive/folders/1sQYlzC0NK73HIWJoy-H9i0EbwoaccX4I?usp=drive_link

### Overleaf (LaTeX source, view-only)
https://www.overleaf.com/read/ztmbfrnsfygr#30e992

### LaTeX Source in this Submission
The complete LaTeX source is in the `Latex_Source/` folder of this zip:
- `main.tex` — main paper file
- `references.bib` — bibliography
- `figure-scene.jpeg`, `figure-raycast.jpeg`, `figure-unity.jpeg` — figures

To recompile: open in Overleaf or run `pdflatex → bibtex → pdflatex → pdflatex`.

---

## How to Run the Application

### Hardware required
- Meta Quest 2 headset
- A computer running Unity 6 (or later) with Android build support installed

### Steps
1. Clone the GitHub repo or download the Unity project files from the Google Drive link above
2. Open the project in Unity 6
3. Open `BasicScene` from `Assets/Scenes/`
4. Connect a Meta Quest 2 via USB cable (developer mode enabled), or build to APK and install on the headset
5. Press Play in the Unity Editor with Quest Link active, or run the installed APK on the headset

### Controls
- **Ray-casting mode (default):** point with the controller laser, pull the trigger to grab ingredients, release over the pot or trash to drop
- **Head-gaze mode:** turn your head to aim the reticle at an ingredient, press a button on the controller to confirm the selection

### Recipe gameplay
Each recipe lists required ingredients on the recipe panel above the cooking station. Pick up the matching ingredient and drop it into the pot. Wrong ingredients can be discarded into the trash. Complete the recipe by adding all required ingredients in any order.

---

## Literature Survey

All 24 academic sources cited in the paper are included in the `PDFs-LiteratureSurvey/` folder of this zip.

Sources span:
- VR selection technique research (ray-casting, head-gaze, hybrid methods)
- Qualitative methodology (thematic analysis, interview saturation)
- VR usability and presence
- Hand-tracking vs. controller comparisons
- VR teleoperation and remote interaction

---

## Work Allocation

- **Zachary Kinnaman** — most of the writing and the research paper
- **Ziqiu Feng** — most of the coding (Unity scripts, VR interaction logic, recipe system) and recorded most of the videos (code overview, project overview, gameplay)
- **Kyle Wong** — 3D modeling and scene configuration in Unity (kitchen layout, ingredient props, asset arrangement)

All three group members contributed to study design, running interviews, and thematic analysis.

A more detailed who-did-what breakdown appears at the end of the programming video.

---

## Other Notes

- **Sample size:** 6 participants (qualitative track per the rubric option)
- **All participants were VR novices** (no prior headset experience). This is a real limitation of the study and is addressed in the Limitations section of the paper.
- **No eye tracking:** the Quest 2 does not support eye tracking, so our "head-gaze" condition is head-orientation only. The paper makes this explicit.
- **If any video link fails for the grader, please contact the group at the emails above and we will provide an alternate link immediately.**

---

## Submission Contents

```
Astro_Kitchen/
├── README.md                              ← this file
├── Astro_Kitchen_Final_Report.pdf         ← compiled final paper
├── Latex_Source/                          ← Overleaf-downloaded LaTeX project
│   ├── main.tex
│   ├── references.bib
│   ├── figure-scene.jpeg
│   ├── figure-raycast.jpeg
│   └── figure-unity.jpeg
├── PDFs-LiteratureSurvey/                 ← all 24 academic source PDFs
└── Source_Code/
    └── github-and-drive-links.txt         ← link to GitHub and Google Drive
```
