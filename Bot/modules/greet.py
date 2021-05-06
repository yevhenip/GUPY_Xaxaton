from aiogram import Dispatcher
from aiogram.types import Message

from dependency_injector.wiring import inject, Provide

from container import DiContainer
from services.events import EventsService


@inject
async def user_start(msg: Message, event_service: EventsService = Provide[DiContainer.events_service]):
    await event_service.get_events()
    await msg.reply("Hello, world!")


def register_greet_module(dispatcher: Dispatcher):
    dispatcher.register_message_handler(user_start, commands=["start"], state="*")
