using System.Text;
using System.Text.Json;
//using Newtonsoft.Json;

public class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, Gemini!");

        //var prompt = "바이든이 사퇴했어. 앞으로 미국 대선은 어떤 양상으로 전개될까? 뉴스 앵커같은 말투로 알려줘.";
        var prompt = "하하 그거 그러케 하는거 아닌데 ㅋㅋㄹㅃㅃ";

        GenerativeLanguageApi gemini = new();

        Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}::prompt = {prompt}");

        await foreach (var content in gemini.streamGenerateContentAsync(prompt))
        {
            if (!string.IsNullOrEmpty(content))
            {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}::{content}");
            }
        }

        //var jsonData = System.Text.Json.JsonSerializer.Serialize(data);
        //Console.WriteLine($"{data}");

        Console.WriteLine("Bye bye~!");
    }
}

/************************************************************************/

public class GenerativeLanguageApi
{
    public async IAsyncEnumerable<string> streamGenerateContentAsync(string _prompt)
    {
        string model = "gemini-1.5-pro";
        string apiKey = Secret.googleGenerativeLanguageApiKey;
        string url = $"https://generativelanguage.googleapis.com/v1/models/{model}:generateContent?key={apiKey}";

        var httpClient = new HttpClient();

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

        StringBuilder currentEvent = new StringBuilder();

        string line;
        string content = string.Empty;

        while ((line = await reader.ReadLineAsync()) != null)
        {
            //Console.WriteLine(line);
            yield return line;

            //if (string.IsNullOrEmpty(line))
            //{
            //    if (currentEvent.Length > 0)
            //    {
            //        content = ProcessEvent(currentEvent.ToString());

            //        if (!string.IsNullOrEmpty(content))
            //        {
            //            yield return content;
            //        }

            //        currentEvent.Clear();
            //    }
            //}
            //else
            //{
            //    currentEvent.AppendLine(line);
            //}
        }
    }

    private string ProcessEvent(string eventData)
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

                if (jsonResponse.TryGetProperty("candidates", out var candidates) &&
                    candidates.GetArrayLength() > 0 &&
                    candidates[0].TryGetProperty("content", out var content) &&
                    content.TryGetProperty("parts", out var parts) &&
                    parts.GetArrayLength() > 0 &&
                    parts[0].TryGetProperty("text", out var text))
                {
                    return text.GetString();
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON 파싱 오류: {ex.Message}");
            }
        }

        return string.Empty;
    }
}