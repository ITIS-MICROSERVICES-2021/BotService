#install python 3.4
#{python3.4_directory}\Scripts\pip.exe install python-telegram-bot==11.0.0

import sys
print(sys.version)
print("hello world")

from telegram.ext import Updater, CommandHandler
from telegram import Bot, ParseMode
import logging

token='2018371884:AAEJbN3m1_NhaMP65Gv6ayUF_Lc3y2bdxvY'
updater = Updater(token=token)
bot = Bot(token=token)

dispatcher = updater.dispatcher

logging.basicConfig(format='%(asctime)s - %(name)s - %(levelname)s - %(message)s',
                     level=logging.INFO)


def start(bot, update):
    print()
    print(update)
    print(update.message.chat.id)
    
    bot.send_message(
        update.message.chat.id,
        text="Приходите в мой дом, мои двери открыты",
        parse_mode=ParseMode.HTML
    )

start_handler = CommandHandler('start', start)
dispatcher.add_handler(start_handler)

updater.start_polling()

print("aaa")
input("Press enter to exit ;)")