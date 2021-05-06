from aiogram import Dispatcher
from aiogram.types import Message


async def user_start(m: Message):
    await m.reply("Hello, world!")


def register_greet_module(dispatcher: Dispatcher):
    dispatcher.register_message_handler(user_start, commands=["start"], state="*")
