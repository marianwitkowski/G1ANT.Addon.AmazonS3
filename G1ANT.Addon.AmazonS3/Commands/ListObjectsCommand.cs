﻿using System;
using System.Collections.Generic;
using G1ANT.Language;


namespace G1ANT.Addon.AmazonS3
{
    [Command(Name = "amazons3.listobjects", Tooltip = "List bucket content")]
    public class ListObjectsCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Bucket name")]
            public TextStructure bucketName { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }

        public ListObjectsCommand(AbstractScripter scripter) : base(scripter)
        {
        }


        public void Execute(Arguments arguments)
        {
            try
            {
                List<String> keys = S3Settings.GetInstance().awsS3.ListingObjects(arguments.bucketName.Value);
                ListStructure result = new ListStructure(new List<object>());
                if (keys != null)
                {
                    foreach (string key in keys)
                    {
                        result.AddItem(new TextStructure(key));
                    }
                }
                //String flatStr = string.Join(";", keys.ToArray());
                Scripter.Variables.SetVariableValue(arguments.Result.Value, result);
            }
            catch (Exception exc)
            {
                throw new ApplicationException(exc.Message);
            }
        }
    }
}
