using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;
using YamlDotNet.RepresentationModel;

[InitializeOnLoad]
public static class ShowTeam
{
    static ShowTeam()
    {
        var team = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Exercice/Equipe.yaml");
        if (team == null)
        {
            Debug.LogWarning("Réimportation d'assets, SVP redémarrer l'éditeur");
            return;
        }

        var yaml = new YamlStream();
        yaml.Load(new StringReader(team.text));
        var root = yaml.Documents[0].RootNode as YamlMappingNode;
        var equipe = root?["equipe"] as YamlMappingNode;
        if (equipe == null || equipe.Children.Count == 0)
        {
            EditorUtility.DisplayDialog("Identification", "N'oubliez pas d'indiquer le nom de vos coéquipiers dans le fichier \"Assets/Exercice/Equipe.yaml\"!!", "Compris!");
            return;
        }

        var teamMembers = new StringBuilder();
        teamMembers.AppendLine("Coéquipiers:");
        var codeMatch = new Regex(@"^[A-Z]{4}[0-9]{8}$");
        foreach (var item in equipe.Children)
        {
            var code = (item.Key as YamlScalarNode).Value;
            var name = (item.Value as YamlScalarNode).Value;
            if (!codeMatch.IsMatch(code))
            {
                EditorUtility.DisplayDialog("Identification", $"Code permanent invalide: {code}!", "Compris!");
                return;
            }
            teamMembers.AppendLine($"- {code}: {name}");
        }

        Debug.Log(teamMembers);
    }
}
