from aiogram import Dispatcher
from aiogram.dispatcher import FSMContext
from aiogram.dispatcher.filters.state import State, StatesGroup
from aiogram.types import Message

from dependency_injector.wiring import inject, Provide

from container import DiContainer
from services.events import EventsService


class EventListStates(StatesGroup):
    scrolling = State()

def get_navigation_keyboard():
    pass

@inject
async def list_events(msg: Message, state: FSMContext,
                      event_service: EventsService = Provide[DiContainer.events_service]):
    await EventListStates.scrolling.set()

    events = await event_service.get_events()


def register_event_list_module(dispatcher: Dispatcher):
    dispatcher.register_message_handler(list_events, commands=["events"], state="*")
