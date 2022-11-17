using Microsoft.EntityFrameworkCore;
using RandomNumberBot.Entity;
using RandomNumberBot.Repo;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RandomNumberBot
{
    public static class OnUpdateMethod
    {
        public static string[] Sticker = new string[] { "🥇", "🥈", "🥉" };
        public static async Task OnMessageReceived(ITelegramBotClient botClient, Message message)
        {

            var chatId = message.Chat.Id;
            if (!DefaultValues.admins.Contains(chatId))
                return;

            if (message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Assalomu alaykum.Tasodifiy tanlshni amalga oshirish uchun Random ni bosing!",
                    replyMarkup: ReplyMarkup.SendKeyBoard()
                    );
            }
            if (message.Text == "Random")
            {
                var userRepo = new UserRepositoryAsync();
               // var reandomIndex = await userRepo.RandomUser();
                StringBuilder textBuilder = new StringBuilder();

                var user = await userRepo.RandomAndGetUser();
                if(user == null)
                {
                    await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Foydalanuvchi topilmadi"
                    );
                    return;
                }

                foreach(var comment in user.Comments)
                {
                    textBuilder.Append(comment + "\n\n");
                }
                string comments = textBuilder.ToString();

                if (string.IsNullOrEmpty(comments))
                {
                    comments = "Taklif kelmagan";
                }

                string text = "Tanlov raqami 🔑:" + user.VoterNumber + "\n\n" +
                              "Foydalanuvchi 👤 : " + user.FullName+ "\n\n"+
                              "Takliflar 💼: \n " + textBuilder.ToString();

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: text
                    );
                
            }
            else if (message.Text == "Delete User")
            {
                await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Haqiqatdan ham o'chirmoqchimisiz ?",
                        replyMarkup: ReplyMarkup.DeletedInlineButton()
                        );
            }
        }
        public static async Task OnCallBackQueryReceived(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            var callback = callbackQuery.Data;
            var chatId = callbackQuery.From.Id;

            if (!DefaultValues.admins.Contains(chatId))
                return;

            if (callback == "Deleted True")
            {
                var userRepo = new UserRepositoryAsync();
                bool isSuccess = await userRepo.CopyAndDelete();

                await botClient.DeleteMessageAsync(
                        callbackQuery.Message.Chat,
                        callbackQuery.Message.MessageId
                        ); 

                if (isSuccess)
                {
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "O'chirildi va boshqa jadvalga yozildi"
                        );
                }
                else
                {
                    await botClient.SendTextMessageAsync(
                       chatId: chatId,
                       text: "Xatolik sodir bo'ldi"
                       );
                }
            }
            else if (callback == "Deleted False")
            {
                await botClient.DeleteMessageAsync(
                        callbackQuery.Message.Chat,
                        callbackQuery.Message.MessageId
                        );
            }
        }
    }
}
