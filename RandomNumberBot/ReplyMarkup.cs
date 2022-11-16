using Telegram.Bot.Types.ReplyMarkups;

namespace RandomNumberBot
{
    public static class ReplyMarkup
    {

        public static IReplyMarkup SendKeyBoard()
        {
            return new ReplyKeyboardMarkup(new[]
                   {
                        new KeyboardButton[] { "Random" },
                        new KeyboardButton[] { "Delete User" }
                   })
                   {
                    ResizeKeyboard = true
                   };
        }

        public static IReplyMarkup DeletedInlineButton()
        {
            return new InlineKeyboardMarkup(new[]
           {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text:"Ha ✅",callbackData: "Deleted True"),
                    InlineKeyboardButton.WithCallbackData(text:"Yo'q ❌",callbackData: "Deleted False"),
                }
            });
        }
    }
}
