using System.Data;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

GenerativeLanguageApi g = new();
g.Generate();

/************************************************************************/

public class GenerativeLanguageApi
{
    public async Task Generate()
    {
        try
        {
            //string keyFilePath = @"gemini-cm-svc-acc-jungwoo.json";
            string projectId = "gemini-cm";
            string location = "us-central1"; // Iowa(아이오와)
            string token = string.Empty;
            string model = "gemini-1.5-pro";
            string apiKey = Secret.googleGenerativeLanguageApiKey;

            // HTTP 클라이언트 설정
            using HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));
            //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json; charset=UTF-8");

            // 요청 URL 설정
            string url = $"https://generativelanguage.googleapis.com/v1/models/{model}:generateContent?key={apiKey}";

            // 요청 메시지 생성
            var prompt = "안녕 반가워. 너랑 친구가 되고 싶어. 친구랑 대화하듯이 친근한 말투와 반말로 대답해줄래? 너 이름이 뭐니?";
            prompt = "Why is the sky blue ?";
            //Logger.log($"{prompt}");

            var requestContent = GeneratePayload(prompt);

            //var jsonRequestContent = JsonConvert.SerializeObject(requestContent);
            var httpContent = new StringContent(requestContent, Encoding.UTF8, "application/json");

            /////////////////////////////////////////////////////////////////

            //HttpMessage http = new HttpMessage(url, "application/json; charset=UTF-8", "text/event-stream", null);

            //Stream responseStream = await http.HttpPostAsyncStream("", requestContent);

            //using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
            //{
            //    // 응답 스트림을 비동기적으로 읽어오기
            //    char[] buffer = new char[8192];
            //    int charsRead;
            //    while ((charsRead = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
            //    {
            //        string content = new string(buffer, 0, charsRead);
            //        Console.Write(content);
            //    }
            //}

            var a = 1;
            var zero = 0;
            //var b = a / zero;

            /////////////////////////////////////////////////////////////////
            
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            httpRequestMessage.Content = httpContent;

            // API 호출 및 스트림 응답 처리
            var response = await httpClient.SendAsync(httpRequestMessage);
            //var response = await httpClient.PostAsync(url, httpContent).ConfigureAwait(true);

            //response.EnsureSuccessStatusCode(); // HTTP 응답에 대한 IsSuccessStatusCode 속성이 false이면 예외를 throw

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            using (var streamReader = new System.IO.StreamReader(responseStream))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = await streamReader.ReadLineAsync();
                    if (!string.IsNullOrEmpty(line) && line.StartsWith("data:"))
                    {
                        var jsonData = line.Substring(5).Trim();
                        Console.WriteLine($"line.Substring(5) = {jsonData}");
                    }
                }
            }
        }
        catch (HttpIOException hx)
        {
            Console.WriteLine($"{hx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    private string GeneratePayload(string prompt)
    {
        var part = new Part
        {
            Text = prompt,
        };

        var content = new Content
        {
            Role = "USER",
            Parts = new List<Part> { part }
        };

        var root = new Root
        {
            Contents = new List<Content> { content }
        };

        string json = JsonConvert.SerializeObject(root, Newtonsoft.Json.Formatting.Indented);

        Console.WriteLine($"{json}");

        return json;
    }
}

public class Part
{
    public string Text { get; set; }
}

public class Content
{
    public string Role { get; set; }
    public List<Part> Parts { get; set; }
}

public class Root
{
    public List<Content> Contents { get; set; }
}