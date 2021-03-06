import asyncio
import logging

import modules

from aiogram import Bot, Dispatcher
from aiogram.contrib.fsm_storage.memory import MemoryStorage

from config import BOT_TOKEN
from container import DiContainer
from modules.common import register_common_module
from modules.event_list import register_event_list_module


async def main():
    container = DiContainer()
    container.wire(packages=[modules])

    bot = Bot(token=BOT_TOKEN)
    logging.basicConfig(level=logging.INFO)

    storage = MemoryStorage()
    dispatcher = Dispatcher(bot, storage=storage)

    register_common_module(dispatcher)
    register_event_list_module(dispatcher)

    try:
        await dispatcher.start_polling()
    finally:
        await bot.close()


if __name__ == '__main__':
    asyncio.run(main())
