export interface GeminiRequest {
    apiKey: string;
    prompt: string;
    model: string;
}

interface GeminiPart {
    text?: string;
}

interface GeminiResponse {
    candidates?: Array<{
        content?: {
            parts?: GeminiPart[];
        };
    }>;
    error?: {
        message?: string;
    };
}

export async function generateGeminiContent(request: GeminiRequest): Promise<string> {
    const { apiKey, prompt, model } = request;
    const endpoint = `https://generativelanguage.googleapis.com/v1beta/models/${encodeURIComponent(model)}:generateContent?key=${encodeURIComponent(apiKey)}`;

    const response = await fetch(endpoint, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            contents: [
                {
                    parts: [{ text: prompt }]
                }
            ]
        })
    });

    const payload = await response.json() as GeminiResponse;

    if (!response.ok) {
        throw new Error(payload.error?.message ?? "Gemini request failed.");
    }

    const text = payload.candidates
        ?.flatMap(candidate => candidate.content?.parts ?? [])
        .map(part => part.text ?? "")
        .join("\n")
        .trim();

    if (!text) {
        throw new Error("Gemini returned an empty response.");
    }

    return text;
}

