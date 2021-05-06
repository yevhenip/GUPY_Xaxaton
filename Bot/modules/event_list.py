from aiogram import Dispatcher
from aiogram.dispatcher import FSMContext
from aiogram.dispatcher.filters.state import State, StatesGroup
from aiogram.types import Message, ReplyKeyboardMarkup, InlineKeyboardMarkup, InlineKeyboardButton, ParseMode, \
    CallbackQuery

from dependency_injector.wiring import inject, Provide

from container import DiContainer
from services.events import EventsService


class EventListStates(StatesGroup):
    scrolling = State()


def get_navigation_keyboard():
    keyboard = ReplyKeyboardMarkup(resize_keyboard=True)
    options = ["/previous", "/next"]
    keyboard.add(*options)
    keyboard.row("/cancel")

    return keyboard


@inject
async def show_page(page_number: int, msg: Message, event_service: EventsService = Provide[DiContainer.events_service]):
    events = await event_service.get_events(page_number)

    for event in events:
        event_type = "Онлайн" if event.type == 0 else "Оффлайн"

        reactions = [
            InlineKeyboardButton(text=f"👍 Піду!", callback_data=f"{event.id}")
        ]

        markup = InlineKeyboardMarkup()
        markup.add(*reactions)

        await msg.answer(
            f"📎 Назва: <b>{event.name}</b>\n\n"
            f"📅 Дата проведення: <i>{event.event_time}</i>\n"
            f"⌚ Тривалість: <i>{event.duration}</i> год.\n"
            f"⭕ Тип: {event_type}\n"
            f"🧍 Людей підуть: {event.subscribed_count}\n\n"
            f"📜 Опис:\n<code>{event.description}</code>",
            reply_markup=markup,
            parse_mode=ParseMode.HTML)


async def list_events(msg: Message,
                      state: FSMContext):
    await EventListStates.scrolling.set()
    await state.update_data(currentPage=1)

    keyboard = get_navigation_keyboard()
    await msg.answer("Показую івенти:", reply_markup=keyboard)
    await show_page(1, msg)


async def handle_event_subscription(callback: CallbackQuery):
    # call api and add person to event
    await callback.answer(text="Тебе було додано до івенту! Перевір особистий кабінет (/myevents)",
                          show_alert=True)


async def next_page(msg: Message, state: FSMContext):
    user_data = await state.get_data()

    new_page = user_data["currentPage"] + 1
    await state.update_data(currentPage=new_page)

    await show_page(new_page, msg)


async def prev_page(msg: Message, state: FSMContext):
    user_data = await state.get_data()

    if user_data["currentPage"] == 1:
        await msg.answer("Ти на першій сторінці ☺️")
        return

    new_page = user_data["currentPage"] - 1
    await state.update_data(currentPage=new_page)

    await show_page(new_page, msg)


def register_event_list_module(dispatcher: Dispatcher):
    dispatcher.register_message_handler(list_events, commands=["events"], state="*")
    dispatcher.register_message_handler(next_page, commands=["next"], state=EventListStates.scrolling)
    dispatcher.register_message_handler(prev_page, commands=["previous"], state=EventListStates.scrolling)
    dispatcher.register_callback_query_handler(handle_event_subscription, state=EventListStates.scrolling)
