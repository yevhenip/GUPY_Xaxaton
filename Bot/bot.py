import asyncio
import logging

from aiogram import Bot, Dispatcher
from aiogram.contrib.fsm_storage.memory import MemoryStorage

from config import BOT_TOKEN
from modules.greet import register_greet_module


async def main():
    bot = Bot(token=BOT_TOKEN)
    logging.basicConfig(level=logging.INFO)

    storage = MemoryStorage()
    dispatcher = Dispatcher(bot, storage=storage)

    register_greet_module(dispatcher)

    try:
        await dispatcher.start_polling()
    finally:
        await bot.close()


if __name__ == '__main__':
    asyncio.run(main())
