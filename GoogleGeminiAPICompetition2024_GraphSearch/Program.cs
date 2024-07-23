using System.Data;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Xml;
using Newtonsoft.Json;

public class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        
        GenerativeLanguageApi gemini = new();
        var data = await gemini.generateContentAsync("우주는 왜 검은 색이야? 초등학생에게 알려주듯 친근한 말투와 쉬운 내용으로 설명해줘.");
        //var jsonData = System.Text.Json.JsonSerializer.Serialize(data);
        Console.WriteLine($"ret = [{data}]");
        Console.WriteLine("Bye bye~");
    }
}

/************************************************************************/

public class GenerativeLanguageApi
{
    public async Task<StringBuilder> generateContentAsync(string _prompt)
    {
        string model = "gemini-1.5-pro";
        string apiKey = Secret.googleGenerativeLanguageApiKey;
        string url = $"https://generativelanguage.googleapis.com/v1/models/{model}:generateContent?key={apiKey}";

        var httpClient = new HttpClient();

        StringBuilder currentEvent = new StringBuilder();

        try
        {
            var requestBody = new
            {
                contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = _prompt }
                    }
                }
            },
                generationConfig = new
                {
                    temperature = 0.9,
                    topK = 1,
                    topP = 1,
                    maxOutputTokens = 2048,
                    stopSequences = new string[] { }
                },
                safetySettings = new object[] { },
            };

            var jsonContent = System.Text.Json.JsonSerializer.Serialize(requestBody);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = httpContent,
                Headers = { { "Accept", "text/event-stream" } }
            };

            using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);

            string line;
            //StringBuilder currentEvent = new StringBuilder();

            while ((line = await reader.ReadLineAsync()) != null)
            {
                //Console.WriteLine($"line = {line}");

                if (string.IsNullOrEmpty(line))
                {
                    if (currentEvent.Length > 0)
                    {
                        //ProcessEvent(currentEvent.ToString());
                        currentEvent.Clear();
                    }
                }
                else
                {
                    currentEvent.AppendLine(line);
                }
            }

            return currentEvent;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return currentEvent;
    }

    void ProcessEvent(string eventData)
    {
        var lines = eventData.Split('\n');
        string data = "";

        foreach (var line in lines)
        {
            if (line.StartsWith("data: "))
            {
                data = line.Substring(6);
                break;
            }
        }

        if (!string.IsNullOrEmpty(data) && data != "[DONE]")
        {
            try
            {
                var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(data);

                if (jsonResponse.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0 &&
                    candidates[0].TryGetProperty("content", out var content) &&
                    content.TryGetProperty("parts", out var parts) && parts.GetArrayLength() > 0 &&
                    parts[0].TryGetProperty("text", out var text)
                    )
                {
                    Console.Write(text.GetString());
                }
            }
            catch (System.Text.Json.JsonException ex)
            {
                Console.WriteLine($"JSON parsing error: {ex.Message}");
            }
        }
    }
}