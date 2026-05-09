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

### Project Overview Video (3–5 min, high-level intro to the project)
- View link: [NEEDS FILL-IN — record and upload]
- Download link: [NEEDS FILL-IN]

### Project Code Video (programming walkthrough + who-did-what breakdown)
- View link: https://colostate-my.sharepoint.com/:v:/g/personal/ziqiuf_colostate_edu/IQD-uye2e-x1S5Cn_hXLgdWuAdeaBHqXmJGy7_HHo_fEpKQ?e=Wimykt
- Download link: [NEEDS FILL-IN — open the SharePoint link, click "..." menu, copy the direct download URL]
- **IMPORTANT: confirm the SharePoint link is set to "Anyone with the link can view" — test in an incognito tab before submitting.**

### Project Presentation Video (~12 minutes recommended)
- View link: [NEEDS FILL-IN — needs to be recorded]
- Download link: [NEEDS FILL-IN]

### Gameplay Demonstration (Meta Horizon share)
- https://horizon.meta.com/shares/VfSWEyO8JiPVyr9eSC8ZrbL9J3RBv6
- This is a Meta Horizon share link of the prototype in action. Open in any browser.

---

## Repository / Code Access

### Google Drive (Unity project — full source)
The Unity project is too large for GitHub due to asset packages, so the full project files are hosted on Google Drive:

https://drive.google.com/drive/folders/15cHUKeBVjdOaMH0dNbuETKHDuV-uW0oU

This folder also contains the video files. **Confirm sharing is set to "Anyone with the link can view" before submitting.**

### GitHub Repository
[NEEDS FILL-IN — if you have one, paste the URL. If not, mention here that the full source is on the Google Drive linked above.]

### Overleaf (LaTeX source)
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
1. Download the Unity project files from the Google Drive link above
2. Open the project in Unity 6
3. Open the `BasicScene` from `Assets/Scenes/`
4. Connect a Meta Quest 2 via USB cable (developer mode enabled) or build to an APK
5. Press Play in the Unity Editor with Quest Link active, or build and install on the headset

### Controls
- **Ray-casting mode (default):** point with the controller laser, pull the trigger to grab ingredients, release over the pot/trash to drop
- **Head-gaze mode:** turn your head to aim the reticle at an ingredient, press a button on the controller to confirm the selection

### Recipe gameplay
Each recipe lists required ingredients (shown on the recipe panel above the cooking station). Pick up the ingredient that matches the current required item and drop it into the pot. Wrong ingredients can be discarded into the trash. Complete the recipe by adding all required ingredients in any order.

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

### Technical Demo / Prototype Development
- [NEEDS FILL-IN — list who did what on the Unity prototype]

### Videos
- Code overview video: Ziqiu Feng (recorded by Ziqiu)
- Gameplay demonstration: Ziqiu Feng and Kyle Wong
- Project overview video: [NEEDS FILL-IN]
- Presentation video: [NEEDS FILL-IN]

### Research Paper
- [NEEDS FILL-IN — list who wrote which sections, who did interviews, who did thematic analysis, etc.]

### Study Participants and Interviews
- [NEEDS FILL-IN — who recruited, who ran sessions, who did the analysis]

A more detailed who-did-what breakdown appears at the end of the programming video.

---

## Other Notes

- **Sample size:** 6 participants (qualitative track per the rubric option)
- **All participants were VR novices** (no prior headset experience). This is a real limitation of the study and is addressed in the Limitations section of the paper.
- **No eye tracking:** the Quest 2 does not support eye tracking, so our "head-gaze" condition is head-orientation only. The paper makes this explicit.
- **The Unity project includes the BasicScene that was used during participant testing.** The same scene supports both selection techniques (toggled in code).
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
│   ├── ahmed2025-thematic-analysis.pdf
│   ├── argelaguet2013-3d-selection-survey.pdf
│   ├── ... (22 more)
└── Source_Code/
    └── github-link.txt                    ← link to Google Drive (Unity project too large to include directly)
```
