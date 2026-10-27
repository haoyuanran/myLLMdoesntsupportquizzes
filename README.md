# myLLMdoesntsupportquizzes

100% vibecoded app that lets LLMs with no built-in quiz functionality generate quizzes with a formatted JSON file.

**JSON Quiz** turns a JSON file into an interactive quiz with multiple-choice questions and flip-card flashcards.
The format is simple enough for Claude, ChatGPT or Gemini to write. See [QUIZ_FORMAT.md](QUIZ_FORMAT.md).

## Run it

| How | What to do |
|---|---|
| **Windows app** | Double-click `dist\JsonQuiz.exe`. It opens in its own window. To open a quiz straight away, drag a `.json` file onto the exe, or right-click the file → *Open with* → JsonQuiz.exe. |
| **Browser (offline)** | Double-click `index.html`. Everything runs locally, and no internet connection is needed. |
| **On the web** | Upload `index.html` to any static host (GitHub Pages, Netlify, Cloudflare Pages…). To link a specific quiz, add `?quiz=<url-of-json>` to the address (the JSON host must allow cross-origin requests, e.g. a GitHub raw link). |

## Making a quiz with an AI

1. On the home screen, under **Make a quiz with AI**, type a topic.
2. Pick a chatbot from the dropdown (ChatGPT, Claude, Gemini, Copilot, Perplexity, Grok, DeepSeek, Mistral, Meta AI) and click **Copy prompt & open**. It opens in a popup window beside the quiz with the prompt copied to your clipboard. Where the site supports it, the prompt is already typed in for you.
3. Copy the chatbot's reply, paste it under **Load the quiz** and click **Load pasted quiz**. Code fences and extra chat text are fine.

## Features

- Multiple choice with instant feedback, explanations, hints and "select all that apply" questions
- Flashcards with a flip animation that you mark as *Knew it* or *Still learning*
- Shuffle questions and answers, or filter to only MCQs or only flashcards
- Results screen showing what you missed, with a one-click **Retry the ones I missed**
- Recent quizzes are remembered on this device
- Keyboard shortcuts: `A`–`D` / `1`–`4` to answer, `Enter` / `→` for next, `Space` to flip
- Light and dark themes

## Files

- `index.html`: the whole app in a single file
- `dist/JsonQuiz.exe`: Windows launcher with the app embedded inside it
- `launcher/Program.cs` + `build.bat`: source for the exe. Run `build.bat` after editing `index.html` to rebuild it. It uses the C# compiler built into Windows, so nothing needs installing.
- `examples/sample-quiz.json`: an example quiz

The exe needs Microsoft Edge (built into Windows 10/11) or Chrome, and uses it to draw its window.
