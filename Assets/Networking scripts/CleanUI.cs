using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

static class CleanUI
{
   public static string CleanString(string input)
   {
       if (string.IsNullOrEmpty(input))
       {
           return string.Empty;
        }
       input = Regex.Unescape(input).Replace("\r\n", "\n");

        int cut = input.IndexOf("\n", System.StringComparison.Ordinal);
        if (cut >= 0) input = input[..cut];
        {
            
        }

        return input.Trim();
    }
}
