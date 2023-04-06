// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PerformanceImprovements
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> resumes = new List<string>();
            // Fill resumes with sample data (100 resumes)
            for (int i = 0; i < 100; i++)
            {
                resumes.Add("Sample resume data " + i);
            }

            // Without performance improvements
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<string[]> skillsList1 = new List<string[]>();
            foreach (string resume in resumes)
            {
                skillsList1.Add(FindSkillsInResume(resume));
            }
            stopwatch.Stop();
            Console.WriteLine($"Without performance improvements\nexecution time: {stopwatch.ElapsedMilliseconds} ms");

            // With performance improvements
            stopwatch.Restart();
            List<string[]> skillsList2 = new List<string[]>(resumes.Count);
            for (int i = 0; i < resumes.Count; i++)
            {
                skillsList2.Add(null);
            }
            Parallel.ForEach(resumes, (resume, _, index) =>
            {
                skillsList2[(int)index] = FindSkillsInResume(resume);
            });
            stopwatch.Stop();
            Console.WriteLine($"\nWith performance improvements\nexecution time: {stopwatch.ElapsedMilliseconds} ms");
        }

        public static string[] FindSkillsInResume(string resume)
        {
            // This function takes 200ms to find skills in resume
            System.Threading.Thread.Sleep(200);
            return new string[] { "Sample skill 1", "Sample skill 2" };
        }
    }
}

