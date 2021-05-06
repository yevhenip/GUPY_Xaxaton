from aiogram import Dispatcher
from aiogram.dispatcher import FSMContext
from aiogram.types import Message, ReplyKeyboardRemove, ParseMode


async def start(msg: Message, state: FSMContext):
    await state.finish()
    await msg.answer("Привіт! Радий тебе бачити у GUPY.Events! Тут ти можеш організувати або продивитись активні "
                     "івенти. Для початку, обери що ти хочеш: організувати або продивитись активні. Тут все анонімно, "
                     "не переймайся, ти тут як свій)")


async def cancel(msg: Message, state: FSMContext):
    await state.finish()
    await msg.answer("Зрозумів. Відміняю операцію.", reply_markup=ReplyKeyboardRemove())


async def help_(msg: Message):
    await msg.answer("Я почув, що комусь потрібна допомога? Я,бот <b>GUPY.Events</b> , допоможу тобі."
                     "Для того, щоб організувати івент, тобі потрібно клацнути на кнопку <i>'Організувати івент'</i>."
                     "Після чого тобі дадуть вибір типу івента: <b>онлайн</b>/<b>офлайн</b>."
                     "Зробивши вибір <i>типу</i> івента, тобі потрібно зробити <i>опис</i>, просто напиши що буде "
                     "відбуватись та чим цікавий цей івент. Можеш за бажанням додати контактні дані, щоб з тобою "
                     "можна було зв'язатись. "
                     "Після цього тебе спитають, у яку <b>дату</b> та який <b>час</b> буде проводитись івент та його "
                     "тривалість годин/хвилин. "
                     "Ввівши всі дані залишається лише кнопка <i>'Створити івент'</i> після кліка на котру твій івент "
                     "побачать люди. "
                     "Для того, щоб продивитись список івентів, у виборі <i>'Організувати івент/Продивитись "
                     "івенти'</i> тицни кнопку <i>'Продивитись івенти'</i>. "
                     "Після цього ти побачиш у чаті із ботом 5 івентів, у котрих ти можеш клацнути на кнопку "
                     "<i>'Приймаю учать'</i>, щоб організатор знав, що буде ще один прекрасний учасник івенту! "
                     "Якщо серед цього списку нема зацікавівших тебе івентів, ти можеш нажати кнопку <i>'Продивитись "
                     "ще 5 івентів'</i>, після чого тобі в чат з ботом додадуться ще 5 івентів. Можеш клацати до тих "
                     "пір, поки не знайдеш щось цікаве для себе!",
                     parse_mode=ParseMode.HTML)


def register_common_module(dispatcher: Dispatcher):
    dispatcher.register_message_handler(start, commands=["start"], state="*")
    dispatcher.register_message_handler(cancel, commands=["cancel"], state="*")
    dispatcher.register_message_handler(help_, commands=["help"], state="*")
