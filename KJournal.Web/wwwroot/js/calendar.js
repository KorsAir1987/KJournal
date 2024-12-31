const calendarGrid = document.getElementById('calendarGrid');
const monthElement = document.getElementById('month');
const yearElement = document.getElementById('year');
const noteText = document.getElementById('noteText');
const saveNote = document.getElementById('saveNote');
const prevMonth = document.getElementById('prevMonth');
const nextMonth = document.getElementById('nextMonth');
const prevYear = document.getElementById('prevYear');
const nextYear = document.getElementById('nextYear');

const monthNames = [
    'January', 'February', 'March', 'April', 'May',
    'June', 'July', 'August', 'September', 'October', 'November', 'December'
];

const notes = {};
let currentDate = new Date();
let selectedDate = new Date();

function renderCalendar() {
    const year = currentDate.getFullYear();
    const month = currentDate.getMonth();
    const firstDayOfMonth = new Date(year, month, 1);
    const lastDayOfMonth = new Date(year, month + 1, 0);
    const firstDayOfWeek = firstDayOfMonth.getDay();
    const daysInMonth = lastDayOfMonth.getDate();

    const daysFromPrevMonth = firstDayOfWeek;
    const totalDays = 42; // Always show 6 rows (6 weeks)
    const days = [];

    // Days from the previous month
    const prevMonthLastDay = new Date(year, month, 0).getDate();
    for (let i = daysFromPrevMonth - 1; i >= 0; i--) {
        days.push({
            date: new Date(year, month - 1, prevMonthLastDay - i),
            otherMonth: true
        });
    }

    // Current month days
    for (let i = 1; i <= daysInMonth; i++) {
        days.push({
            date: new Date(year, month, i),
            otherMonth: false
        });
    }

    // Days from the next month to fill the grid
    for (let i = days.length; i < totalDays; i++) {
        days.push({
            date: new Date(year, month + 1, i - daysInMonth - daysFromPrevMonth + 1),
            otherMonth: true
        });
    }

    // Render grid
    calendarGrid.innerHTML = '';
    days.forEach(day => {
        const dayElement = document.createElement('div');
        dayElement.classList.add('calendar-day');
        if (day.otherMonth) dayElement.classList.add('other-month');
        if (day.date.toDateString() === new Date().toDateString()) dayElement.classList.add('current-day');
        if (day.date.toDateString() === selectedDate.toDateString()) dayElement.classList.add('selected-day');
        if (notes[day.date.toDateString()]) dayElement.classList.add('has-note');
        dayElement.textContent = day.date.getDate();
        dayElement.addEventListener('click', () => selectDate(day.date));
        calendarGrid.appendChild(dayElement);
    });

    // Update header
    monthElement.textContent = monthNames[month];
    yearElement.textContent = year;
}
function selectDate(date) {
    selectedDate = date;
    noteText.value = notes[date.toDateString()] || '';
    renderCalendar();
}

function changeMonth(offset) {
    currentDate.setMonth(currentDate.getMonth() + offset);
    renderCalendar();
}

function changeYear(offset) {
    currentDate.setFullYear(currentDate.getFullYear() + offset);
    renderCalendar();
}

saveNote.addEventListener('click', () => {
    const note = noteText.value.trim();
    const dateKey = selectedDate;

    if (note) {
        notes[dateKey] = note;
    } else {
        delete notes[dateKey];
    }

    // Call the function to save the note
    saveNoteToServer(dateKey, note)
        .then(data => {
            console.log('Note saved successfully:', data);
            renderCalendar(); // Update the calendar after saving the note
        })
        .catch(error => {
            console.error('Error saving note:', error);
        });
});

function saveNoteToServer(date, note) {
    const postData = {
        date: date.toISOString(),
        note: note || null, // Send `null` if note is deleted
    };

    return fetch('/Calendar/SaveNote', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(postData),
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to save the note');
            }
            return response.json();
        });
}

prevMonth.addEventListener('click', () => changeMonth(-1));
nextMonth.addEventListener('click', () => changeMonth(1));
prevYear.addEventListener('click', () => changeYear(-1));
nextYear.addEventListener('click', () => changeYear(1));

// Initial render
renderCalendar();