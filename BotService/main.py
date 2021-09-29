#install python 3.4
#{python3.4_directory}\Scripts\pip.exe install python-telegram-bot==11.0.0

import sys
print(sys.version)
print("hello world")

from telegram.ext import Updater, CommandHandler
import logging

updater = Updater(token='2018371884:AAEJbN3m1_NhaMP65Gv6ayUF_Lc3y2bdxvY')

dispatcher = updater.dispatcher

logging.basicConfig(format='%(asctime)s - %(name)s - %(levelname)s - %(message)s',
                     level=logging.INFO)

def start(update, context):
    print(update)
    print(context)
    #context.bot.send_message(chat_id=update.effective_chat.id, text="I'm a bot, please talk to me!")

start_handler = CommandHandler('start', start)
dispatcher.add_handler(start_handler)

updater.start_polling()

print("aaa")
input("Press enter to exit ;)")

""" dispatcher = updater.dispatcher

def start(update, context):
    context.bot.send_message(chat_id=update.effective_chat.id, text="I'm a bot, please talk to me!")

start_handler = CommandHandler('start', start)
dispatcher.add_handler(start_handler)

print("bbb")
updater.start_polling()
print("ccc") """


""" 
from telegram import Update
from telegram.ext import Updater, CommandHandler, CallbackContext

def hello(update: Update, context: CallbackContext) -> None:
    update.message.reply_text('Hello!!' + update.effective_user.first_name)

updater = Updater('AAEJbN3m1_NhaMP65Gv6ayUF_Lc3y2bdxvY')

updater.dispatcher.add_handler(CommandHandler('hello', hello))

updater.start_polling()
updater.idle() """
