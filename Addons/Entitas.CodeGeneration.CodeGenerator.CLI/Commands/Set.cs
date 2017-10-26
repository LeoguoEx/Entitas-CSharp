﻿using Entitas.Utils;
using Fabl;

namespace Entitas.CodeGeneration.CodeGenerator.CLI {

    public class Set : AbstractCommand {

        public override string trigger { get { return "set"; } }
        public override string description { get { return "Sets the value of a key"; } }
        public override string example { get { return "entitas set key value"; } }

        public override void Run(string[] args) {
            if (args.Length == 3) {
                if (assertPreferences()) {
                    var preferences = loadPreferences();
                    var key = args[1];
                    var value = args[2];
                    if (preferences.properties.HasKey(key)) {
                        Helper.AddValueSilently(
                            value,
                            new string[0],
                            values => preferences[key] = values.ToCSV(),
                            preferences);
                    } else {
                        Helper.AddKey("Key doesn't exist. Do you want to add", key, value, preferences);
                    }
                }
            } else {
                fabl.Warn("The set command expects exactly two arguments");
                fabl.Info("E.g. entitas set Entitas.CodeGeneration.Plugins.Contexts Game");
            }
        }
    }
}