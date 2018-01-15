﻿using BotExample.Model;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Linq;

namespace BotExample.Extensions
{
    public static class CharacterExtensions
    {
        public static IMessageActivity ToMessage(this Character character, IDialogContext context)
        {
            var reply = context.MakeMessage();
            reply.Attachments = new List<Attachment>
            {
                character.ToAttachment(context)
            };

            return reply;
        }

        public static IMessageActivity ToMessage(this IEnumerable<Character> characters, IDialogContext context)
        {
            var reply = context.MakeMessage();
            reply.AttachmentLayout = "carousel";
            reply.Attachments = characters.Select(c => c.ToAttachment(context)).ToList();
            return reply;
        }

        public static Attachment ToAttachment(this Character character, IDialogContext context)
        {
            HeroCard hc = new HeroCard()
            {
                Title = character.Name,
                Subtitle = character.Profession,
                Text = character.Information,
                Images = new List<CardImage>()
                {
                    new CardImage()
                    {
                        Url = character.Imageurl
                    }
                }
            };

            return hc.ToAttachment();
        }
    }
}