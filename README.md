# Exercise Journal App

This is a web app for logging your workouts, reps, and sets built with **ASP.NET Core Razor Pages**, powered by a **third-party exercise API** to fetch exercises, and backed by a **Dockerized MySQL database**.

## What This Does (Besides Judging Your Reps)

- Lets you **search** for exercises using a 3rd-party API.
- View what each exercise targets (muscle-wise, not emotionally).
- **Log** exercises daily, including your sets and reps.
- See your workout history via a stylish Razor Pages UI.
- Hosted in Docker with Docker Compose like a civilized dev.

## Table of Contents

- [Getting Started](#getting-started)
- [How to Run With Docker](#how-to-run-with-docker)
- [Features](#features)
- [Tech Stack](#tech-stack)

---

## Getting Started

Clone the repo like you mean it:

```bash
git clone https://github.com/Istebrek/ExerciseJournal.git
cd ExerciseJournalAPI
```

### Make sure you have the following installed:

- .NET 8 SDK
- Docker
- Docker Compose
- Browser or Postman (for visually admiring your work)

---

## How to Run With Docker
> If you've ever yelled “it works on my machine,” Docker Compose is here to humble you.

1. **Clone the project** (if you haven't already).

2. Build and run using Docker Compose:

```bash
docker-compose up --build
```

3. Open your browser and navigate to:
```bash
http://localhost:5050
```
5. If everything's running, give yourself 10 pushups as a reward.

---

## Features

- **Searchbar**: Type in the name of an exercise (e.g., "Alternate lateral pulldown") and let the 3rd-party API do the lifting.
- **Dynamic Journal**: View and modify your daily workouts.
- **Adjust Reps & Sets**: Because you got stronger overnight.
- **Calendar Picker**: Change the date to track your historical workouts.
- **Target Muscle Display**: Know what you’re hitting (besides your limits).

---

## Tech Stack

- **Frontend**: Razor Pages (ASP.NET Core)
- **Backend**: ASP.NET Core 8
- **Database**: MySQL (Dockerized)
- **Data Source**: External Exercise API
- **Deployment**: Docker + Docker Compose
- **JS**: Vanilla JS for search and journal logic
- **HTML/CSS**: Custom styling and structuring

---


