# Quiz JSON format

A quiz is one JSON object with a `title` and a list of `questions`. Each item is either a
multiple-choice question (`"type": "mcq"`) or a flashcard (`"type": "flashcard"`).

```json
{
  "title": "Short quiz title",
  "description": "Optional one-liner",
  "questions": [
    {
      "type": "mcq",
      "question": "Which planet is closest to the Sun?",
      "options": ["Venus", "Mercury", "Mars", "Earth"],
      "answer": "Mercury",
      "explanation": "Optional: shown after answering.",
      "hint": "Optional: revealed on request."
    },
    {
      "type": "mcq",
      "question": "Which are gas giants? (select all that apply)",
      "options": ["Jupiter", "Mars", "Saturn", "Venus"],
      "answer": ["Jupiter", "Saturn"]
    },
    {
      "type": "flashcard",
      "front": "Astronomical Unit",
      "back": "Average Earth–Sun distance, about 150 million km.",
      "hint": "Optional"
    }
  ]
}
```

## Fields

| Field | Applies to | Required | Notes |
|---|---|---|---|
| `title` | quiz | yes | |
| `description` | quiz | no | |
| `type` | item | yes | `"mcq"` or `"flashcard"` |
| `question` | mcq | yes | |
| `options` | mcq | yes | 2 or more strings |
| `answer` | mcq | yes | The exact text of the correct option. A list of strings means "select all that apply". |
| `explanation` | mcq | no | Shown after answering |
| `front` / `back` | flashcard | yes | |
| `hint` | both | no | Hidden until the player clicks "Show hint" |

Text can use `**bold**`, `*italic*`, `` `code` `` and line breaks (`\n`).

Options that say "All of the above" / "None of the above" are never shuffled.

## The app is forgiving

LLM output varies, so the app also accepts:

- JSON wrapped in ```` ```json ```` fences, or with chatty text before and after it
- trailing commas and curly quotes
- a bare list of questions instead of an object
- separate `"mcq": [...]` and `"flashcards": [...]` lists
- `answer` as a letter (`"B"`), a 0-based index (`1`), or `"B) Mercury"`
- `choices` instead of `options`, `correct_answer` instead of `answer`, `term`/`definition` instead of `front`/`back`
- options written as `"A) Venus"` (the labels are removed) or as `{"text": "...", "correct": true}` objects

Items that still can't be read are skipped and listed on the start screen, so one bad question
won't stop the rest of the quiz from loading.

## Prompt to give an AI

The **Make a quiz with AI** card on the app's home screen builds this for you, with your topic
and your MCQ/flashcard split (set with the slider) filled in. Here it is for copying by hand:

```text
Create a quiz about: [YOUR TOPIC HERE]
Make 11 "mcq" and 4 "flashcard" items.

Reply with only JSON in this format:
{
  "title": "Quiz title",
  "questions": [
    {"type": "mcq", "question": "...", "options": ["...", "...", "...", "..."], "answer": "...", "explanation": "optional"},
    {"type": "flashcard", "front": "...", "back": "..."}
  ]
}
"answer" must exactly match one of the options (use a list if several are correct).
```
