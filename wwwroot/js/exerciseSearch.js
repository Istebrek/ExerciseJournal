if (isNaN(currentDate.getTime())) {
    console.error("Invalid currentDateStr:", currentDateStr);
    currentDate = new Date();
}
const input = document.getElementById("exercise-search");
const resultsDiv = document.getElementById("results-div");

input.addEventListener("input", async () => {
    const query = input.value.trim();
    if (query.length === 0) {
        resultsDiv.innerHTML = "";
        return;
    }

    const response = await fetch(`/api/exercises/search?query=${encodeURIComponent(query)}`);
    const exercises = await response.json();

    resultsDiv.innerHTML = "";
    exercises.forEach(exercise => {
        const button = document.createElement("button");
        button.classList.add("result-buttons");
        button.style.backgroundColor = '#280f91';
        button.style.color = 'white';
        button.style.padding = '8px 16px';
        button.style.borderRadius = '20px';
        button.style.border = 'none';
        button.style.cursor = 'pointer';
        button.style.margin = '5px';
        button.textContent = exercise.name;
        button.onclick = async () => {
            let dateStr = currentDate.toISOString().split("T")[0];
            let dayRes = await fetch(`/api/day/by-date?date=${dateStr}`);
            let dayId = await dayRes.json();
            if (!dayId || dayId <= 0) {
                const createDayRes = await fetch('/api/day', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ date: dateStr })
                });
                dayId = await createDayRes.json();
            }
            const res = await fetch('/api/journal', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    DayId: dayId,
                    Exercise: exercise.name,
                    Target: exercise.target,
                    Repetitions: 0,
                    Sets: 0
                })
            });
            if (res.ok) { location.reload(); }
        };

        resultsDiv.appendChild(button);
    });
});


function updateDate(newDate) {
    const url = new URL(window.location.href);
    url.searchParams.set("CurrentDate", newDate.toISOString().split("T")[0]);
    window.location.href = url.toString();
}

window.changeYear = offset => {
    currentDate.setFullYear(currentDate.getFullYear() + offset);
    updateDate(currentDate);
};

window.changeMonth = offset => {
    currentDate.setMonth(currentDate.getMonth() + offset);
    updateDate(currentDate);
};

window.changeDay = offset => {
    currentDate.setDate(currentDate.getDate() + offset);
    updateDate(currentDate);
};

async function updateCount(journalId, field, delta) {
    const fieldMap = {
        "Repetitions": "reps",
        "Sets": "sets"
    };

    const fieldId = `${fieldMap[field]}-${journalId}`;
    const countElem = document.getElementById(fieldId);
    if (!countElem) {
        console.error("Could not find element with id:", fieldId);
        return;
    }

    let currentValue = parseInt(countElem.textContent);
    const newValue = Math.max(0, currentValue + delta);

    countElem.textContent = newValue;

    const body = {};
    body[field] = newValue;

    try {
        const response = await fetch(`/api/journal/${journalId}`, {
            method: "PATCH",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(body)
        });

        if (!response.ok) {
            console.error("Failed to update journal entry");
            countElem.textContent = currentValue;
        }
    } catch (err) {
        console.error("Error:", err);
        countElem.textContent = currentValue;
    }
}